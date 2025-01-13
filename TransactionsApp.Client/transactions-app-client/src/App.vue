<template>
  <div id="app">
    <AppBar />
    <div class="w-75 m-auto">
      <router-view/>
    </div>
  </div>
</template>

<script>
import {defineComponent} from "vue";
import AppBar from "@/components/AppBar.vue";

export default defineComponent({
  components: {
    AppBar
  }
})
</script>

<style>
</style>
