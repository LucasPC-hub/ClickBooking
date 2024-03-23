<script setup>
import { RouterLink, RouterView } from 'vue-router'
import NavBar from "@/components/navBar.vue";
</script>

<template>
  <nav-bar />
  <div class="h-screen" :style="{ backgroundColor: `#F9F6EE`, backgroundSize: 'cover', backgroundRepeat: 'no-repeat' }">
    <RouterView />
  </div>
</template>

<style scoped>
html, body {
  height: 100%;
  margin: 0;
  padding: 0;
}
</style>