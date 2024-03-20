/** @type {import('tailwindcss').Config} */
export default {
  content: [
    './src/**/*.{vue,js}', // Adicione o caminho para seus arquivos Vue e JS
  ],
  theme: {
    extend: {colors: {
        primary: '#efc114', // Substitua '#123456' pela cor primária desejada
        secondary: '#000000', // Substitua '#654321' pela cor secundária desejada
        tertiary: 'rgba(220,220,220,0.73)', // Substitua '#ffffff' pela cor terciária desejada
      },
      }
  },
  plugins: [
    require('tailwindcss'), // Adicione o tailwindcss aos plugins
    require('autoprefixer'),
  ],
}