<template>
  <div class="flex justify-center items-center h-screen p-10 ">
    <div class="grid md:grid-cols-2 grid-cols-1 bg-tertiary border-8 rounded-3xl">
      <div class="flex flex-col justify-center items-center p-5">
        <form @submit.prevent="handleSubmit">
          <h1 class="text-center mb-10 font-bold text-4xl">Criar Reserva</h1>
          <input v-model="reserva.DataHora" type="datetime-local" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Data e Hora">
          <input v-model="reserva.NumeroPessoas" type="number" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Número de Pessoas">
          <button type="submit" class="  bg-secondary border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary">Criar Reserva</button>
        </form>
      </div>
      <div class="">
        <img v-if="restaurante && restaurante.imagem" :src="`data:image/jpeg;base64,${restaurante.imagem}`" alt="Imagem do Restaurante" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'

const restaurante = ref(null)
const route = useRoute()
const id = route.params.id

onMounted(async () => {
  const response = await axios.get(`/api/restaurante/${id}`)
  restaurante.value = response.data
})
const reserva = ref({
  DataHora: '',
  NumeroPessoas: 0,
  UsuarioId: localStorage.getItem('userId')
})

const handleSubmit = async () => {
  try {
    const response = await axios.post(`/api/restaurante/${id}/addReserva`, reserva.value)
    console.log('Reserva criada com sucesso:', response.data)
  } catch (error) {
    console.error('Erro ao criar reserva:', error)
  }
}
</script>

<style scoped>

</style>