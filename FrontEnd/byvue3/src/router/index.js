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
        component: () => import('../views/client/BlogsList.vue')
    },
    {
        path: '/contact',
        name: 'contact',
        component: () => import('../views/client/ContactPage.vue')
    },
    {
        path: '/photographers',
        name: 'photographers',
        component: () => import('../views/client/PhotographersPage.vue')
    },
    {
        path: '/blogs/:blogId',
        name: 'blogdetail',
        component: () => import('../views/client/BlogDetail.vue')
    },

    {
        path: '/post/create',
        name: 'createpost',
        component: () => import('../views/client/CreatePost.vue'),
    },
    {
        path: '/user/:username',
        name: 'userprofile',
        component: () => import('../views/client/UserProfile.vue'),
        children: [
            {
                path: '',
                component: () => import('../views/client/UserProfile/HomeProfile.vue')
            },
            {
                path: 'blogs',
                component: () => import('../views/client/UserProfile/UserBlogs.vue'),

            },
            {
                path: 'posts',
                component: () => import('../views/client/UserProfile/UserPosts.vue'),
            },
            {
                path: 'posts/:postId',
                component: () => import('../views/client/UserProfile/UserPostDetail.vue'),
            },
            {
                path: 'recent-comments',
                component: () => import('../views/client/UserProfile/UserRecentComments.vue')
            },
            {
                path: 'followers',
                component: () => import('../views/client/UserProfile/UserFollowers.vue')
            },
            {
                path: 'followings',
                component: () => import('../views/client/UserProfile/UserFollowing.vue')
            },
            {
                path: 'categories',
                component: () => import('../views/client/UserProfile/UserCategories.vue')
            },
            {
                path: 'trendings',
                component: () => import('../views/client/UserProfile/UserTrending.vue')
            },
            {
                path: 'selling',
                component: () => import('../views/client/UserProfile/UserSelling.vue')
            },
            {
                path: 'purchase-history',
                component: () => import('../views/client/UserProfile/UserPurchaseHistory.vue')
            },
            {
                path: 'jobs',
                component: () => import('../views/client/UserProfile/UserJobs.vue')
            },
            {
                path: 'settings',
                component: () => import('../views/client/UserProfile/UserSettings.vue')
            }
        ]
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
    },
    {
        path: '/unauthorized',
        name: 'unauthorized',
        component: () => import('../views/exceptionpages/NotAuthorized.vue')
    }
    , {
        path: '/:catchAll(.*)',
        name: 'notfound',
        component: () => import('../views/exceptionpages/PageNotFound.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
