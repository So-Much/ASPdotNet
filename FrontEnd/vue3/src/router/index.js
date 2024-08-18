import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/views/HomePage.vue';
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomePage
  },
  {
    path: '/login',
    name: 'login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/LoginPage.vue')
  },
  {
    path: '/register',
    name: 'register',
    component: () => import('../views/RegisterPage.vue')
  },
  {
    path: '/portfolio',
    name: 'portfolio',
    component: () => import('../views/PortfolioPage.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
