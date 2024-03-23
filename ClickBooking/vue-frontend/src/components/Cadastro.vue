<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
const router = useRouter() // Define router

const username = ref('')
const password = ref('')
const confirmPassword = ref('')
const errorMessages = ref([])// Alterado para uma string

const handleSubmit = async () => {
  if (password.value !== confirmPassword.value) {
    alert('As senhas não são iguais.')
    return
  }

  const data = {
    username: username.value,
    password: password.value,
  }

  try {
    const response = await axios.post('https://localhost:7201/api/user/register', data)
    if (response.status === 200) {
      router.push('/')
    }
  } catch (error) {
    console.error(error)
    errorMessages.value = error.response.data // Atribui o array de mensagens de erro
  }
}

const goToLogin = () => {
  router.push('/')
}
</script>

<template>
  <div class="flex  justify-center items-center h-screen p-10 ">
    <div class="grid md:grid-cols-2 grid-cols-1 bg-tertiary border-8 rounded-3xl">
      <div class="flex flex-col justify-center items-center p-5">
        <img src="/assets/logo.webp" class="h-52 transform -translate-y-32" alt="logo">
        <h1 class="text-center font-bold text-6xl transform -translate-y-32">ClickBooking</h1>
        <form @submit.prevent="handleSubmit">
          <h1 class="text-center mb-10 font-bold text-4xl">Realize seu Cadastro</h1>
          <input v-model="username" type="text" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Username">
          <input v-model="password" type="password" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Senha">
          <input v-model="confirmPassword" type="password" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Confirme sua senha">
          <button type="submit" class="  bg-secondary border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary">Cadastre-se</button>
          <button @click.prevent="goToLogin" type="button" class=" bg-secondary mt-8  border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary"> Ja possuo uma conta</button>
          <!-- Exibe a mensagem de erro -->
          <ul >
            <li class="p-1 bg-red-600 text-xl rounded text-sky-50cd" v-for="(error, index) in errorMessages" :key="index">{{ error.description }}</li>
          </ul>
        </form>
      </div>
      <div class="">
        <img src="/assets/login.webp" class="rounded-3xl"  alt="">
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>