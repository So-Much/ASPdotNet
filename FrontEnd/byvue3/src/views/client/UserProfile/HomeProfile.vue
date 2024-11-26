<script setup>
import { axios } from '@/configs';
import { onBeforeMount, reactive, ref } from 'vue';
import { useLoading } from 'vue-loading-overlay';
import { useToast } from 'vue-toastification';

const $loading = useLoading();
const toast = useToast();

onBeforeMount( () => {
  axios.get('/api/user/information',
							{
								headers: {
									Authorization: 'Bearer ' + localStorage.getItem('token')
								}
							}
						)
							.then(
								res => {
									if (res.status === 200) {
                    avatar.value = res.data.avatar;
									}
								}
							)
})

const user = reactive({
  username: '',
  userID: '',
  email: '',
  contact: {
    phoneNumber: '',
    address: ''
  }
});

const originalUser = reactive({
  username: '',
  userID: '',
  email: '',
  contact: {
    phoneNumber: '',
    address: ''
  }
});

const avatar = ref("")

if (localStorage.getItem('token')) {
  axios.get('/api/user/information', {
    headers: {
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }
  })
    .then(res => {
      if (res.status === 200) {
        console.log(res.data);
        user.username = res.data.name;
        user.userID = res.data.uid;
        user.email = res.data.email;
        if (res.data.contact) {
          user.contact.phoneNumber = res.data.contact.phoneNumber || '';
          user.contact.address = res.data.contact.address || '';
        }
        // Copy data to originalUser
        Object.assign(originalUser, JSON.parse(JSON.stringify(user)));
      }
    });
}

const SubmidChangeUserInfor = () => {
  const hasChanged = JSON.stringify(user) !== JSON.stringify(originalUser);
  if (hasChanged) {
    axios.post('/api/user/update', user, {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }
    })
      .then(res => {
        if (res.status === 200) {
          console.log('User information updated successfully');
          // Update originalUser to the new values
          Object.assign(originalUser, JSON.parse(JSON.stringify(user)));
        }
      })
      .catch(err => {
        console.error('Error updating user information:', err);
      });
  } else {
    console.log('No changes detected');
  }
};

const uploadavatar = (e) => {
  const avatarFileUpload = e.target.files[0];
  console.log(avatarFileUpload);
  if (avatarFileUpload) {
    const formData = new FormData();
    formData.append('avatar', avatarFileUpload)
    if (localStorage.getItem('token')) {
      const loader = $loading.show();
      axios.post('/api/User/uploadavatar', formData,
        {
          headers: {
            Authorization: 'Bearer ' + localStorage.getItem('token'),
            "Content-Type": "multipart/form-data"
          },
        }
      )
        .then(res => {
          console.log(res);
          console.log(res.data);
          avatar.value = res.data;
          loader.hide();
        })
        .catch((err) => {
          console.log(err);
          loader.hide();
        });
    } else {
      toast.error("Token is Expired!");
    }
  }
}

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
                    <img :src="avatar ? avatar : '/png-transparent-default-avatar-thumbnail.png'"
                      @error="e => e.target.src = '/png-transparent-default-avatar-thumbnail.png'" alt="user-avatar"
                      class="d-block rounded img avatar-xl" height="200" id="uploadedAvatar" />
                    <div class="button-wrapper">
                      <label for="upload" class="btn btn-primary me-2 mb-4" tabindex="0">
                        <span class="d-none d-sm-block">Upload new photo</span>
                        <i class="bx bx-upload d-block d-sm-none"></i>
                        <input type="file" id="upload" class="account-file-input" hidden accept="image/png, image/jpeg"
                          @input="uploadavatar" />
                      </label>
                      <button type="button" class="btn btn-outline-secondary account-image-reset mb-4">
                        <i class="bx bx-reset d-block d-sm-none"></i>
                        <span class="d-none d-sm-block">Reset</span>
                      </button>

                      <p class="text-muted mb-0">Allowed JPG, GIF or PNG. Max size of 800K</p>
                    </div>
                  </div>
                </div>
                <hr class="my-0" />
                <div class="card-body">
                  <form id="formAccountSettings" method="POST" onsubmit="return false">
                    <div class="row">
                      <div class="mb-3 col-md-12">
                        <label for="username" class="form-label">User Name</label>
                        <input class="form-control" type="text" id="username" name="username" placeholder="John Doe"
                          v-model="user.username" />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="email" class="form-label">E-mail</label>
                        <input class="form-control" type="text" id="email" name="email" v-model="user.email"
                          placeholder="john.doe@example.com" />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="organization" class="form-label">Organization</label>
                        <input type="text" class="form-control" id="organization" name="organization"
                          value="ThemeSelection" disabled />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label class="form-label" for="phoneNumber">Phone Number</label>
                        <div class="input-group input-group-merge">
                          <span class="input-group-text">US (+1)</span>
                          <input type="text" id="phoneNumber" name="phoneNumber" class="form-control"
                            placeholder="202 555 0111" v-model="user.contact.phoneNumber" />
                        </div>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="address" class="form-label">Address</label>
                        <input type="text" class="form-control" id="address" name="address" placeholder="Address"
                          v-model="user.contact.address" />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="state" class="form-label">State</label>
                        <input class="form-control" type="text" id="state" name="state" placeholder="California"
                          disabled>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="zipCode" class="form-label">Zip Code</label>
                        <input type="text" class="form-control" id="zipCode" name="zipCode" placeholder="231465"
                          maxlength="6" disabled />
                      </div>
                      <div class="mb-3 col-md-6">
                        <label class="form-label" for="country">Country</label>
                        <select id="country" class="select2 form-select" disabled>
                          <option value="">Select</option>
                          <option value="Australia">Australia</option>
                          <option value="Bangladesh">Bangladesh</option>
                          <option value="Belarus">Belarus</option>
                          <option value="Brazil">Brazil</option>
                          <option value="Canada">Canada</option>
                          <option value="China">China</option>
                          <option value="France">France</option>
                          <option value="Germany">Germany</option>
                          <option value="India">India</option>
                          <option value="Indonesia">Indonesia</option>
                          <option value="Israel">Israel</option>
                          <option value="Italy">Italy</option>
                          <option value="Japan">Japan</option>
                          <option value="Korea">Korea, Republic of</option>
                          <option value="Mexico">Mexico</option>
                          <option value="Philippines">Philippines</option>
                          <option value="Russia">Russian Federation</option>
                          <option value="South Africa">South Africa</option>
                          <option value="Thailand">Thailand</option>
                          <option value="Turkey">Turkey</option>
                          <option value="Ukraine">Ukraine</option>
                          <option value="United Arab Emirates">United Arab Emirates</option>
                          <option value="United Kingdom">United Kingdom</option>
                          <option value="United States">United States</option>
                          <option value="Vietnamese">Vietnamese</option>
                        </select>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="language" class="form-label">Language</label>
                        <select id="language" class="select2 form-select" disabled>
                          <option value="">Select Language</option>
                          <option value="en">English</option>
                          <option value="fr">French</option>
                          <option value="de">German</option>
                          <option value="pt">Portuguese</option>
                        </select>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="timeZones" class="form-label">Timezone</label>
                        <select id="timeZones" class="select2 form-select" disabled>
                          <option value="">Select Timezone</option>
                          <option value="-12">(GMT-12:00) International Date Line West</option>
                          <option value="-11">(GMT-11:00) Midway Island, Samoa</option>
                          <option value="-10">(GMT-10:00) Hawaii</option>
                          <option value="-9">(GMT-09:00) Alaska</option>
                          <option value="-8">(GMT-08:00) Pacific Time (US & Canada)</option>
                          <option value="-8">(GMT-08:00) Tijuana, Baja California</option>
                          <option value="-7">(GMT-07:00) Arizona</option>
                          <option value="-7">(GMT-07:00) Chihuahua, La Paz, Mazatlan</option>
                          <option value="-7">(GMT-07:00) Mountain Time (US & Canada)</option>
                          <option value="-6">(GMT-06:00) Central America</option>
                          <option value="-6">(GMT-06:00) Central Time (US & Canada)</option>
                          <option value="-6">(GMT-06:00) Guadalajara, Mexico City, Monterrey</option>
                          <option value="-6">(GMT-06:00) Saskatchewan</option>
                          <option value="-5">(GMT-05:00) Bogota, Lima, Quito, Rio Branco</option>
                          <option value="-5">(GMT-05:00) Eastern Time (US & Canada)</option>
                          <option value="-5">(GMT-05:00) Indiana (East)</option>
                          <option value="-4">(GMT-04:00) Atlantic Time (Canada)</option>
                          <option value="-4">(GMT-04:00) Caracas, La Paz</option>
                        </select>
                      </div>
                      <div class="mb-3 col-md-6">
                        <label for="currency" class="form-label">Currency</label>
                        <select id="currency" class="select2 form-select" disabled>
                          <option value="">Select Currency</option>
                          <option value="usd">USD</option>
                          <option value="euro">Euro</option>
                          <option value="pound">Pound</option>
                          <option value="bitcoin">Bitcoin</option>
                        </select>
                      </div>
                    </div>
                    <div class="mt-2">
                      <button type="submit" class="btn btn-primary me-2" @click.prevent="SubmidChangeUserInfor">Save
                        changes</button>
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