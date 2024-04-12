<template>
  <div class="flex justify-center items-center h-screen p-10 ">
    <div class="grid md:grid-cols-2 grid-cols-1 bg-tertiary border-8 rounded-3xl">
      <div class="flex flex-col justify-center items-center p-5">
        <h1 class="text-center font-bold text-6xl transform -translate-y-32">ClickBooking</h1>
        <form @submit.prevent="handleSubmit">
          <h1 class="text-center mb-10 font-bold text-4xl">Cadastro de Restaurante</h1>
          <input v-model="nome" type="text" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Nome do Restaurante">
          <input v-model="endereco" type="text" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Endereço">
          <input v-model="lugaresDisponiveis" type="number" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Número de Lugares Disponíveis">
          <input v-model="username" type="text" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Username">
          <input v-model="password" type="password" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Senha">
          <input type="file" @change="handleFileUpload" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3">
          <button type="submit" class="  bg-secondary border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary">Cadastrar Restaurante</button>
        </form>
      </div>
      <div class="">
        <img src="/assets/login.webp" class="rounded-3xl"  alt="">
      </div>
    </div>
  </div>
</template>

<script setup>
import {nextTick, ref} from 'vue'
import axios from 'axios'
import router from "@/router/index.js";

const nome = ref('')
const endereco = ref('')
const lugaresDisponiveis = ref('')
const username = ref('')
const password = ref('')
const file = ref(null)

const handleFileUpload = event => {
  file.value = event.target.files[0]
}

const cadastrarRestaurante = async () => {
  let formData = new FormData()
  formData.append('nome', nome.value)
  formData.append('endereco', endereco.value)
  formData.append('lugaresDisponiveis', lugaresDisponiveis.value)

  // Ler o arquivo de imagem como um Blob e convertê-lo em um array de bytes
  const reader = new FileReader()
  reader.onload = async function() {
    const arrayBuffer = this.result
    const array = new Uint8Array(arrayBuffer)
    formData.append('file', new Blob([array])) // Adicione o array de bytes como 'Imagem'

    try {
      const response = await axios.post('https://localhost:7201/api/restaurante/create', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
      if (response.status === 200) {
        return response.data.id // Retorne o ID do restaurante criado
      }
    } catch (error) {
      console.error(error)
    }
  }
  reader.readAsArrayBuffer(file.value)
}

const cadastrarGerente = async (restauranteId) => {
  const data = {
    username: username.value,
    password: password.value,
    restauranteId: restauranteId // Use o ID do restaurante como parâmetro
    
  }

  try {
    const response = await axios.post('https://localhost:7201/api/user/register', data)
    if (response.status === 200) {
      // Redirecionar para a página de login ou mostrar uma mensagem de sucesso
    }
  } catch (error) {
    console.error(error)
  }
  await router.push('/restaurantes')

  await nextTick()

  // Reload the page
  window.location.reload()
}

const handleSubmit = async () => {
  const restauranteId = await cadastrarRestaurante() // Cadastre o restaurante e obtenha o ID
  await cadastrarGerente(restauranteId) // Cadastre o gerente com o ID do restaurante
}
</script>

<style scoped>
/* Adicione aqui o seu CSS */
</style>