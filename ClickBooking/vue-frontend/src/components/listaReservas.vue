<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import moment from 'moment'

const restaurantes = ref([])
const userId = localStorage.getItem('userId')

onMounted(async () => {
  try {
    const response = await axios.get('/api/restaurante')
    restaurantes.value = response.data.filter(restaurante =>
        restaurante.reservas.some(reserva => reserva.usuarioId === userId)
    )
  } catch (error) {
    console.error('Erro ao buscar restaurantes:', error)
  }
})

const cancelarReserva = async (reservaId) => {
  try {
    await axios.post(`/api/reserva/cancel/${reservaId}`)
    // Atualizar a lista de restaurantes após o cancelamento da reserva
    const response = await axios.get('/api/restaurante')
    restaurantes.value = response.data.filter(restaurante =>
        restaurante.reservas.some(reserva => reserva.usuarioId === userId)
    )
  } catch (error) {
    console.error('Erro ao cancelar reserva:', error)
  }
}
</script>

<template>
  <div class="flex flex-wrap gap-4 p-6 justify-center text-lg font-serif">
    <div v-for="restaurante in restaurantes" :key="restaurante.id">
      <a href="#" class="bg-gray-100 flex-grow text-black border-l-8 border-green-500 rounded-md px-3 py-2 w-full md:w-5/12 lg:w-3/12">
        {{ restaurante.nome }}
        <div class="text-gray-500 font-thin text-sm pt-1" v-for="reserva in restaurante.reservas" :key="reserva.id">
          <div>Número de pessoas: {{ reserva.numeroPessoas }}</div>
          <div>Data da reserva: {{ moment(reserva.data).format('DD/MM/YYYY') }}</div>
          <button class="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded" @click="cancelarReserva(reserva.id)">
            Cancelar Reserva
          </button>
        </div>
      </a>
    </div>
  </div>
</template>