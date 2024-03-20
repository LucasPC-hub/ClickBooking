<template>
  <div>
    <h1>Restaurantes</h1>
    <ul>
      <li v-for="restaurante in restaurantes" :key="restaurante.id">
        {{ restaurante.nome }}
        {{restaurante.reservas}}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      restaurantes: []
    };
  },
  async created() {
    const response = await fetch('/api/restaurante/');
    this.restaurantes = await response.json();
  }
};
</script>