import Vue from 'vue'
import VueRouter from 'vue-router'
import NewTransaction from "@/components/TransactionForm.vue";
import TransactionsList from "@/components/TransactionsList.vue";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'TransactionsList',
    component: TransactionsList
  },
  {
    path: '/new',
    name: 'NewTransaction',
    component: NewTransaction
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
