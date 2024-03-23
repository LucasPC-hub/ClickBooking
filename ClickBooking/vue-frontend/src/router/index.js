import { createRouter, createWebHistory } from 'vue-router'
import Login from "@/components/Login.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Login
    },
    {
      path: '/restaurantes',
      name: 'restaurantes',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/ListaRestaurantes.vue')
    },
    {
      path: '/cadastro',
      name: 'cadastro',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/Cadastro.vue')
    },
    {
      path: '/cadastroRestaurante',
      name: 'cadastroRestaurante',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/cadastroRestaurante.vue')
    }
  ]
})

export default router
