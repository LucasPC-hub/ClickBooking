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
import { ref } from 'vue'
import axios from 'axios'

const nome = ref('')
const endereco = ref('')
const lugaresDisponiveis = ref('')
const file = ref(null)

const handleFileUpload = event => {
  file.value = event.target.files[0]
}

const handleSubmit = async () => {
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
        // Redirecionar para a página de restaurantes ou mostrar uma mensagem de sucesso
      }
    } catch (error) {
      console.error(error)
      if (error.response && error.response.data) {
        if (error.response.data.errors) {
          console.log('Erros de validação:', error.response.data.errors)
        } else {
          console.log('Objeto de erro do servidor:', error.response.data)
        }
      }
    }
  }
  reader.readAsArrayBuffer(file.value)
}
</script>

<style scoped>
/* Adicione aqui o seu CSS */
</style>