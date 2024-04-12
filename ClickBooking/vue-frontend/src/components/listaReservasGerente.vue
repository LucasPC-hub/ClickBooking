<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import moment from 'moment'

const reservas = ref([])
const restauranteId = localStorage.getItem('restauranteId') // Obtenha o ID do restaurante do gerente logado

console.log('restauranteId:', restauranteId) // Log restauranteId

onMounted(async () => {
  try {
    const response = await axios.get(`/api/restaurante/${restauranteId}`)
    console.log('response.data:', response.data) // Log response.data
    reservas.value = response.data.reservas
    console.log('reservas:', reservas.value) // Log reservas
  } catch (error) {
    console.error('Erro ao buscar reservas:', error)
  }
})

const cancelarReserva = async (reservaId) => {
  try {
    await axios.post(`/api/reserva/cancel/${reservaId}`)
    // Atualizar a lista de reservas após o cancelamento da reserva
    const response = await axios.get(`/api/restaurante/${restauranteId}`)
    reservas.value = response.data
  } catch (error) {
    console.error('Erro ao cancelar reserva:', error)
  }
}
</script>

<template>
  <div class="flex flex-wrap gap-4 p-6 justify-center text-lg font-serif">
    <div v-for="reserva in reservas" :key="reserva.id">
      <a href="#" class="bg-gray-100 flex-grow text-black border-l-8 border-green-500 rounded-md px-3 py-2 w-full md:w-5/12 lg:w-3/12">
        Número de pessoas: {{ reserva.numeroPessoas }}
        <div class="text-gray-500 font-thin text-sm pt-1">
          Data da reserva: {{ moment(reserva.data).format('DD/MM/YYYY') }}
          <button class="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded" @click="cancelarReserva(reserva.id)">
            Cancelar Reserva
          </button>
        </div>
      </a>
    </div>
  </div>
</template>