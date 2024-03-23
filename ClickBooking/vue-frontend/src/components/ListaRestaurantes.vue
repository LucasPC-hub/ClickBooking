<template>
  <div class="m-12 bg-tertiary">
    <h1 class="text-center text-6xl mb-5">Restaurantes</h1>
    <Swiper class="swiper" :modules="modules" pagination>
      <SwiperSlide v-for="(restaurante, index) in restaurantes.slice(0, 5)" :key="index">
        <img class="rounded-t-lg h-52 w-96" v-if="restaurante.imagem" :src="`data:image/jpeg;base64,${restaurante.imagem}`" @error="handleImageError" alt="" />
      </SwiperSlide>
    </Swiper>
    <div class="grid-container">
      <div v-for="restaurante in restaurantes" :key="restaurante.id" class=" w-60 text-primary border border-gray-200 rounded-lg shadow dark:bg-gray-950 dark:border-gray-700">
        <a href="#">
          <img class="rounded-t-lg h-52 w-96" v-if="restaurante.imagem" :src="`data:image/jpeg;base64,${restaurante.imagem}`" @error="handleImageError" alt="" />
        </a>
        <div class="p-5">
          <a href="#">
            <h5 class="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{{restaurante.nome}}</h5>
          </a>
          <p class="mb-3 font-normal text-gray-700 dark:text-gray-400">{{restaurante.endereco}}</p>
          <a href="#" class="inline-flex items-center px-3 py-2 text-sm font-medium text-center text-secondary bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-primary dark:hover:bg-blue-700 dark:focus:ring-blue-800">
            Fazer Reserva
            <svg class="rtl:rotate-180 w-3.5 h-3.5 ms-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 5h12m0 0L9 1m4 4L9 9"/>
            </svg>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Swiper, SwiperSlide } from 'vue-awesome-swiper'
import { Pagination } from 'swiper'
import 'swiper/swiper-bundle.css'
import 'swiper/css/pagination'

export default {
  components: {
    Swiper,
    SwiperSlide
  },
  data() {
    return {
      restaurantes: [],
      modules: [Pagination]
    };
  },
  methods: {
    handleImageError(e) {
      console.error('Erro ao carregar a imagem:', e);
    }
  },
  async created() {
    const response = await fetch('/api/restaurante/');
    const data = await response.json();
    console.log('Todos os restaurantes retornados pela API:', data);
    this.restaurantes = data;
  }
};
</script>

<style scoped>
.grid-container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 50px;
  margin-top: 20px;
}

.carousel {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.swiper-slide {
  display: flex;
  justify-content: center;
  align-items: center;
}

.swiper-slide img {
  width: 1000px;
  height: 600px;
  object-fit: cover;
  margin: auto;
  border-radius: 10px;
}

h1 {
  font-family: 'Arial', sans-serif;
  color: #333;
}

p {
  font-family: 'Arial', sans-serif;
  color: #666;
}

a {
  color: #0066cc;
  text-decoration: none;
}

a:hover {
  color: #004d99;
}

.max-w-sm {
  max-width: 400px; /* Aumenta a largura máxima dos cards */
  border-radius: 20px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
}
</style>