import Vue from 'vue'
import Vuex from 'vuex'
import axios from "../api";

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    transactions: [],
    transactionToEdit: null,
  },
  getters: {
    transactions: (state) => state.transactions,
    transactionToEdit: (state) => state.transactionToEdit,
  },
  mutations: {
    setTransactions(state, transactions) {
      state.transactions = transactions;
    },
    setTransactionToEdit(state, transaction) {
      state.transactionToEdit = transaction;
    },
    addTransaction(state, transaction) {
      state.transactions.push(transaction);
    },
    updateTransaction(state, updatedTransaction) {
      const index = state.transactions.findIndex(t => t.id === updatedTransaction.id);

      if (index !== -1) {
        state.transactions[index] = updatedTransaction;
      }
    },
    deleteTransaction(state, transactionId) {
      state.transactions = state.transactions.filter(t => t.id !== transactionId);
    },
  },
  actions: {
    async fetchTransactions({ commit }) {
      const response = await axios.get("/transactions");
      commit("setTransactions", response.data);
    },
    async createTransaction({ commit }, payload) {
      const response = await axios.post("/transactions", payload);
      commit("addTransaction", response.data);
    },
    async updateTransaction({ commit }, payload) {
      const response = await axios.put(`/transactions/${payload.id}`, payload);
      commit("updateTransaction", response.data);
    },
    async deleteTransaction({ commit }, transactionId) {
      axios.delete(`/transactions/${transactionId}`);
      commit("deleteTransaction", transactionId);
    },
  },
})
