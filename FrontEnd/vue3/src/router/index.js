import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/views/client/HomePage.vue';
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomePage
  },
  {
    path: '/store',
    name: 'store',
    component: () => import('../views/client/StorePage.vue')
  },
  {
    path: '/login',
    name: 'login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/client/LoginPage.vue')
  },
  {
    path: '/register',
    name: 'register',
    component: () => import('../views/client/RegisterPage.vue')
  },
  {
    path: '/my-portfolio',
    name: 'my-portfolio',
    component: () => import('../views/client/PortfolioPage.vue')
  },
  {
    path: '/blogs',
    name: 'blogs',
    component: () => import('../views/client/BlogsPot.vue')
  },
  // dashboard routes
  {
    path: '/dashboard',
    name: 'dashboard',
    component: () => import('../views/dashboard/DashboardPage.vue'),
  },
  {
    path: '/dashboard/products',
    name: 'admin_products',
    component: () => import('../views/dashboard/ProductsPage.vue'),
  },
  {
    path: '/dashboard/blogs',
    name: 'admin_blogs',
    component: () => import('../views/dashboard/BlogsPage.vue'),
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
