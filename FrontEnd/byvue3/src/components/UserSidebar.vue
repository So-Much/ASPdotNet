<script setup>
import { ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const userId = route.params.userId;


const username = ref("John Doe");
const userRole = ref("Customer");
</script>

<template>
  <div id="nav-bar">
    <input id="nav-toggle" type="checkbox" />
    <div id="nav-header"><a id="nav-title" href="https://codepen.io" target="_blank">C<i
          class="fab fa-codepen"></i>DEPEN</a><label for="nav-toggle"><span id="nav-toggle-burger"></span></label>
      <hr />
    </div>
    <div id="nav-content">
      <!-- add route to it -->
      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/blogs`">
          <i class="fas fa-solid fa-blog"></i><span>Blogs</span>
        </router-link>
      </div>
      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/posts`">
          <i class="fas fa-images"></i><span>Posts</span>
        </router-link>
      </div>

      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/recent-comments`">
          <i class="fas fa-regular fa-comment"></i><span>Recent Comment</span>
        </router-link>
      </div>

      <hr />
      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/followers`">
          <i class="fas fa-solid fa-users"></i><span>Followers</span>
        </router-link>
      </div>
      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/followings`">
          <i class="fas fa-solid fa-user-plus"></i><span>Followings</span>
        </router-link>
      </div>
      <hr />

      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/categories`">
          <i class="fas fa-solid fa-icons"></i><span>Categories</span>
        </router-link>
      </div>


      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/trendings`">
          <i class="fas fa-chart-line"></i><span>Trending</span>
        </router-link>
      </div>


      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/selling`">
          <i class="fas fa-fire"></i><span>Selling</span>
        </router-link>
      </div>

      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/purchase-history`">
          <i class="fas fa-fire"></i><span>Purchase History</span>
        </router-link>
      </div>


      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/jobs`">
          <i class="fas fa-magic"></i><span>Jobs</span>
        </router-link>
      </div>

      <hr />

      <div class="nav-button">
        <router-link class="nav_side_cus" :to="`/user/${userId}/settings`">
          <i class="fas fa-solid fa-gears"></i><span>Settings</span>
        </router-link>
      </div>

      <div id="nav-content-highlight"></div>
    </div><input id="nav-footer-toggle" type="checkbox" />
    <div id="nav-footer">
      <div id="nav-footer-heading">
        <div id="nav-footer-avatar">
          <img src="https://gravatar.com/avatar/4474ca42d303761c2901fa819c4f2547" />
        </div>
        <div id="nav-footer-titlebox">
          <a @click.prevent="() => { }" id="nav-footer-title" href="/" target="_blank">
            {{ username }}
          </a>
          <span id="nav-footer-subtitle">{{ userRole }}</span>
        </div>
        <label for="nav-footer-toggle"><i class="fas fa-caret-up"></i></label>
      </div>
      <div id="nav-footer-content">
        <textarea class="user_info">
          Hello every body! I'm just a dev with vue js properties, i'm also the be dev with ndoe js using express or using ASP.NET framework woking with api core and manything i can do.
        </textarea>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* :root {
  --background: #9c88ff;
  --navbar-width: 256px;
  --navbar-width-min: 80px;
  --navbar-dark-primary: #18283b;
  --navbar-dark-secondary: #2c3e50;
  --navbar-light-primary: #f5f6fa;
  --navbar-light-secondary: #8392a5;
} */

/* html, body {
  margin: 0;
  background: #9c88ff;
} */

.nav_side_cus {
  width: 100%;
}

#nav-toggle:checked~#nav-header {
  width: calc(80px - 16px);
}

#nav-toggle:checked~#nav-content,
#nav-toggle:checked~#nav-footer {
  width: 80px;
}

#nav-toggle:checked~#nav-header #nav-title {
  opacity: 0;
  pointer-events: none;
  transition: opacity 0.1s;
}

#nav-toggle:checked~#nav-header label[for=nav-toggle] {
  left: calc(50% - 8px);
  transform: translate(-50%);
}

#nav-toggle:checked~#nav-header #nav-toggle-burger {
  background: #f5f6fa;
}

#nav-toggle:checked~#nav-header #nav-toggle-burger:before,
#nav-toggle:checked~#nav-header #nav-toggle-burger::after {
  width: 16px;
  background: #8392a5;
  transform: translate(0, 0) rotate(0deg);
}

#nav-toggle:checked~#nav-content .nav-button span {
  opacity: 0;
  transition: opacity 0.1s;
}

#nav-toggle:checked~#nav-content .nav-button .fas {
  min-width: calc(100% - 16px);
}

#nav-toggle:checked~#nav-footer #nav-footer-avatar {
  margin-left: 0;
  left: 50%;
  transform: translate(-50%);
}

#nav-toggle:checked~#nav-footer #nav-footer-titlebox,
#nav-toggle:checked~#nav-footer label[for=nav-footer-toggle] {
  opacity: 0;
  transition: opacity 0.1s;
  pointer-events: none;
}

#nav-bar {
  position: absolute;
  left: 1vw;
  top: 1vw;
  height: calc(100% - 2vw);
  background: #18283b;
  border-radius: 16px;
  display: flex;
  flex-direction: column;
  color: #f5f6fa;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  overflow: hidden;
  user-select: none;
}

#nav-bar hr {
  margin: 0;
  position: relative;
  left: 16px;
  width: calc(100% - 32px);
  border: none;
  border-top: solid 1px #2c3e50;
}

#nav-bar a {
  color: inherit;
  text-decoration: inherit;
}

#nav-bar input[type=checkbox] {
  display: none;
}

#nav-header {
  position: relative;
  width: 256px;
  left: 16px;
  width: calc(256px - 16px);
  min-height: 80px;
  background: #18283b;
  border-radius: 16px;
  z-index: 2;
  display: flex;
  align-items: center;
  transition: width 0.2s;
}

#nav-header hr {
  position: absolute;
  bottom: 0;
}

#nav-title {
  font-size: 1.5rem;
  transition: opacity 1s;
}

label[for=nav-toggle] {
  position: absolute;
  right: 0;
  width: 3rem;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

#nav-toggle-burger {
  position: relative;
  width: 16px;
  height: 2px;
  background: #18283b;
  border-radius: 99px;
  transition: background 0.2s;
}

#nav-toggle-burger:before,
#nav-toggle-burger:after {
  content: "";
  position: absolute;
  top: -6px;
  width: 10px;
  height: 2px;
  background: #f5f6fa;
  border-radius: 99px;
  transform: translate(2px, 8px) rotate(30deg);
  transition: 0.2s;
}

#nav-toggle-burger:after {
  top: 6px;
  transform: translate(2px, -8px) rotate(-30deg);
}

#nav-content {
  margin: 16px 0;
  padding: 16px 0;
  position: relative;
  flex: 1;
  width: 256px;
  background: #18283b;
  box-shadow: 0 0 0 16px #18283b;
  direction: rtl;
  overflow-x: hidden;
  transition: width 0.2s;
}

#nav-content::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}

#nav-content::-webkit-scrollbar-thumb {
  border-radius: 99px;
  background-color: #D62929;
}

#nav-content::-webkit-scrollbar-button {
  height: 16px;
}

#nav-content-highlight {
  position: absolute;
  left: 16px;
  top: -70px;
  width: calc(100% - 16px);
  height: 54px;
  background: #9c88ff;
  background-attachment: fixed;
  border-radius: 16px 0 0 16px;
  transition: top 0.2s;
}

#nav-content-highlight:before,
#nav-content-highlight:after {
  content: "";
  position: absolute;
  right: 0;
  bottom: 100%;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  box-shadow: 16px 16px #9c88ff;
}

#nav-content-highlight:after {
  top: 100%;
  box-shadow: 16px -16px #9c88ff;
}

.nav-button {
  position: relative;
  margin-left: 16px;
  height: 54px;
  display: flex;
  align-items: center;
  color: #8392a5;
  direction: ltr;
  cursor: pointer;
  z-index: 1;
  transition: color 0.2s;
}

.nav-button span {
  transition: opacity 1s;
}

.nav-button .fas {
  transition: min-width 0.2s;
}

.nav-button:nth-of-type(1):hover {
  color: #18283b;
}

.nav-button:nth-of-type(1):hover~#nav-content-highlight {
  top: 16px;
}

.nav-button:nth-of-type(2):hover {
  color: #18283b;
}

.nav-button:nth-of-type(2):hover~#nav-content-highlight {
  top: 70px;
}

.nav-button:nth-of-type(3):hover {
  color: #18283b;
}

.nav-button:nth-of-type(3):hover~#nav-content-highlight {
  top: 124px;
}

.nav-button:nth-of-type(4):hover {
  color: #18283b;
}

.nav-button:nth-of-type(4):hover~#nav-content-highlight {
  top: 178px;
}

.nav-button:nth-of-type(5):hover {
  color: #18283b;
}

.nav-button:nth-of-type(5):hover~#nav-content-highlight {
  top: 232px;
}

.nav-button:nth-of-type(6):hover {
  color: #18283b;
}

.nav-button:nth-of-type(6):hover~#nav-content-highlight {
  top: 286px;
}

.nav-button:nth-of-type(7):hover {
  color: #18283b;
}

.nav-button:nth-of-type(7):hover~#nav-content-highlight {
  top: 340px;
}

.nav-button:nth-of-type(8):hover {
  color: #18283b;
}

.nav-button:nth-of-type(8):hover~#nav-content-highlight {
  top: 394px;
}
.nav-button:nth-of-type(9):hover {
  color: #18283b;
}

.nav-button:nth-of-type(9):hover~#nav-content-highlight {
  top: 448px;
}
.nav-button:nth-of-type(10):hover {
  color: #18283b;
}

.nav-button:nth-of-type(10):hover~#nav-content-highlight {
  top: 502px;
}
.nav-button:nth-of-type(11):hover {
  color: #18283b;
}

.nav-button:nth-of-type(11):hover~#nav-content-highlight {
  top: 556px;
}

#nav-bar .fas {
  min-width: 3rem;
  text-align: center;
}

#nav-footer {
  position: relative;
  width: 256px;
  height: 54px;
  background: #2c3e50;
  border-radius: 16px;
  display: flex;
  flex-direction: column;
  z-index: 2;
  transition: width 0.2s, height 0.2s;
}

#nav-footer-heading {
  position: relative;
  width: 100%;
  height: 54px;
  display: flex;
  align-items: center;
}

#nav-footer-avatar {
  position: relative;
  margin: 11px 0 11px 16px;
  left: 0;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  overflow: hidden;
  transform: translate(0);
  transition: 0.2s;
}

#nav-footer-avatar img {
  height: 100%;
}

#nav-footer-titlebox {
  position: relative;
  margin-left: 16px;
  width: fit-content;
  display: flex;
  flex-direction: column;
  transition: opacity 1s;
}

#nav-footer-subtitle {
  color: #8392a5;
  font-size: 0.6rem;
}

#nav-toggle:not(:checked)~#nav-footer-toggle:checked+#nav-footer {
  height: 30%;
  min-height: 54px;
}

#nav-toggle:not(:checked)~#nav-footer-toggle:checked+#nav-footer label[for=nav-footer-toggle] {
  transform: rotate(180deg);
}

label[for=nav-footer-toggle] {
  position: absolute;
  right: 0;
  width: 3rem;
  height: 100%;
  display: flex;
  align-items: center;
  cursor: pointer;
  transition: transform 0.2s, opacity 0.2s;
}

#nav-footer-content {
  margin: 0 16px 16px 16px;
  border-top: solid 1px #8392a5;
  padding: 16px 0;
  color: #8392a5;
  font-size: 0.8rem;
  overflow: auto;
}

#nav-footer-content::-webkit-scrollbar {
  width: 2px;
  height: 2px;
}

#nav-footer-content::-webkit-scrollbar-thumb {
  border-radius: 99px;
  background-color: #BCF2F6;
}
#nav-footer-content .user_info {
  width: 100%;
  background-color: transparent;
  color: #8392a5;
  border: none;
  height: 300px;
}
</style>