<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
const router = useRouter() // Define router


const username = ref('')
const password = ref('')

const handleSubmit = async () => {
  const data = {
    username: username.value,
    password: password.value
  }

  try {
    const response = await axios.post('https://localhost:7201/api/user/login', data)
    console.log(response.data)
    if (response.status === 200) {
      router.push('/restaurantes')
    }
  } catch (error) {
    console.error(error)
    alert(error.response.data)
  }
}
</script>

<template>
  <div class="flex  justify-center items-center h-screen p-10 ">
    <div class="grid md:grid-cols-2 grid-cols-1 bg-tertiary border-8 rounded-3xl">
      <div class="flex flex-col justify-center items-center p-5">
        <h1 class="text-center font-bold text-6xl transform -translate-y-32">ClickBooking</h1>
        <form @submit.prevent="handleSubmit">
          <h1 class="text-center mb-10 font-bold text-4xl">Realize seu Login</h1>
          <input v-model="username" type="text" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Username">
          <input v-model="password" type="password" class=" bg-gray-100 border outline-none rounded-md py-3 w-full px-4 mb-3" placeholder="Senha">
          <button type="submit" class="  bg-secondary border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary">Login</button>
          <button type="button" class=" bg-secondary mt-8  border outline-none rounded-md py-3 w-full px-4 font-semibold text-primary">Cadastre-se</button>
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