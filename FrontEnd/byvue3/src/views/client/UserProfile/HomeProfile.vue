<script setup>
import { axios } from '@/configs';
import { onBeforeMount, reactive } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import { useRouter } from 'vue-router';
// import { useRoute, useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const $loading = useLoading();
const toast = useToast();
const router = useRouter();
// const router = useRouter();
// const route = useRoute();

const resetLayout = () => {
  // console.log(route.params);
  // router.push(route.path);
  window.location.reload();
}

const user = reactive({
  avatar: "",
  bio: "",
  contact: {
    location: "",
    phonenumber: ""
  },
  email: "",
  name: "",
  password: "",
  roles: [],
  uid: "",
});

const originalUser = reactive({
  avatar: "",
  bio: "",
  contact: {
    location: "",
    phonenumber: ""
  },
  email: "",
  name: "",
  password: "",
  roles: [],
  uid: "",
});

onBeforeMount(() => {
  document.title = 'User Profile';
  const loader = $loading.show();
  axios.get('/api/user/information', {
    headers: {
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }
  })
    .then(res => {
      loader.hide();
      if (res.status === 200) {
        user.avatar = res.data.avatar;
        user.bio = res.data.bio;
        user.email = res.data.email;
        user.name = res.data.name;
        user.password = res.data.password;
        user.roles = res.data.roles;
        user.uid = res.data.uid;
        // console.log(res.data)
        if (res.data.contact) {
          user.contact.location = res.data.contact.location || '';
          user.contact.phonenumber = res.data.contact.phoneNumber || '';
        }
        // Copy data to originalUser
        Object.assign(originalUser, JSON.parse(JSON.stringify(user)));
      }
    })
    .catch(err => {
      loader.hide();
      router.push('/login');
      toast.error("Token is Expired!");
      console.error("Error fetching user data:", err);
    });
});

const SubmidChangeUserInfor = () => {
  const hasChanged = JSON.stringify(user) !== JSON.stringify(originalUser);
  // resetLayout();
  if (hasChanged) {
    const loader = $loading.show();
    axios.put('/api/user/' + user.uid, {
      "UID": user.uid,
      "Name": user.name,
      "Bio": user.bio,
      "Contact": {
        "Location": user.contact.location,
        "PhoneNumber": user.contact.phonenumber
      }
    }, {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }
    })
      .then(res => {
        if (res.status === 200) {
          loader.hide();
          // console.log(res.data);
          toast.success('User information updated successfully');
          resetLayout();
          // Update originalUser to the new values
          Object.assign(originalUser, JSON.parse(JSON.stringify(user)));
        }
      })
      .catch(err => {
        loader.hide();
        console.error('Error updating user information:', err);
      });
  } else {
    console.log('No changes detected');
  }
};

const uploadavatar = (e) => {
  const avatarFileUpload = e.target.files[0];
  // console.log(avatarFileUpload);
  if (avatarFileUpload) {
    const formData = new FormData();
    formData.append('avatar', avatarFileUpload);
    if (localStorage.getItem('token')) {
      const loader = $loading.show();
      axios.post('/api/User/uploadavatar', formData, {
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem('token'),
          "Content-Type": "multipart/form-data"
        },
      })
        .then(res => {
          // console.log(res);
          // console.log(res.data);
          user.avatar = res.data;
          loader.hide();
          toast.success('Avatar uploaded successfully');
          resetLayout();
        })
        .catch((err) => {
          console.log(err);
          loader.hide();
        });
    } else {
      toast.error("Token is Expired!");
    }
  }
};
</script>

<template>
  <div class="layout-wrapper layout-content-navbar">
    <div class="layout-container ">
      <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
          <div class="row">
            <div class="col-md-12">
              <div class="card mb-4">
                <h5 class="card-header">Profile Details</h5>
                <!-- Account -->
                <div class="card-body">
                  <div class="d-flex align-items-start align-items-sm-center gap-4">
                    <img :src="user.avatar ? user.avatar : '/png-transparent-default-avatar-thumbnail.png'"
                      @error="e => e.target.src = '/png-transparent-default-avatar-thumbnail.png'" alt="user-avatar"
                      class="d-block rounded img avatar-xl" height="200" id="uploadedAvatar" />
                    <div class="button-wrapper">
                      <label for="upload" class="btn btn-primary me-2 mb-4" tabindex="0">
                        <span class="d-none d-sm-block">Upload new photo</span>
                        <i class="bx bx-upload d-block d-sm-none"></i>
                        <input type="file" id="upload" class="account-file-input" hidden accept="image/png, image/jpeg"
                          @input="uploadavatar" />
                      </label>

                      <p class="text-muted mb-0">Allowed JPG, GIF or PNG. Max size of 800K</p>
                    </div>
                  </div>
                </div>
                <hr class="my-0" />
                <div class="card-body">
                  <form id="formAccountSettings" method="POST" @submit.prevent="SubmidChangeUserInfor">
                    <div class="row">
                      <div class="mb-3 col-md-6">
                        <label for="username" class="form-label">User Name</label>
                        <input class="form-control" type="text" id="username" name="username" placeholder="John Doe"
                          v-model="user.name" />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="email" class="form-label">E-mail</label>
                        <input class="form-control" type="text" id="email" name="email" v-model="user.email"
                          placeholder="john.doe@example.com" disabled>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label class="form-label" for="phoneNumber">Phone Number</label>
                        <div class="input-group input-group-merge">
                          <span class="input-group-text">US (+1)</span>
                          <input type="text" id="phoneNumber" name="phoneNumber" class="form-control"
                            placeholder="202 555 0111" v-model="user.contact.phonenumber" />
                        </div>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="address" class="form-label">Address</label>
                        <input type="text" class="form-control" id="address" name="address" placeholder="Address"
                          v-model="user.contact.location" />
                      </div>
                    </div>
                    <div class="mt-2">
                      <button type="submit" class="btn btn-primary me-2">Save changes</button>
                      <button type="reset" class="btn btn-outline-secondary">Cancel</button>
                    </div>
                  </form>
                </div>
                <!-- /Account -->
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<style scoped>
@import '@/assets/vendor/css/core.css';
@import '@/assets/vendor/css/theme-default.css';
@import '@/assets/vendor/css/pages/page-auth.css';
</style>