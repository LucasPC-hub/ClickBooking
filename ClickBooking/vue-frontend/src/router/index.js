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
      path: '/login',
      name: 'login',
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
    },
    {
      path: '/reserva/:id',
      name: 'reserva',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/criarReserva.vue')
    },
    {
      path: '/listaReservas',
      name: 'listaReservas',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/listaReservas.vue')
    },
    {
      path: '/listaReservasGerente',
      name: 'listaReservasGerente',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../components/listaReservasGerente.vue')
    }
  ]
})

export default router
