import { createStore } from 'vuex'

export default createStore({
  state: {
    managerLoggedIn: false,
  },
  mutations: {
    SET_MANAGER_LOGGED_IN(state, value) {
      state.managerLoggedIn = value
    },
  },
  actions: {
    setManagerLoggedIn({ commit }, value) {
      commit('SET_MANAGER_LOGGED_IN', value)
    },
  },
})