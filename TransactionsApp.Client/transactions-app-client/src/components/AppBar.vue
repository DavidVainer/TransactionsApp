<template>
  <b-navbar toggleable="lg" type="dark" variant="primary" class="app-bar">
    <div class="w-75 m-auto d-flex">
      <b-navbar-brand href="/" class="title d-flex gap-2 align-items-center">
        <img alt="Vue logo" src="../assets/logo.png">
        <strong>Transactions App</strong>
      </b-navbar-brand>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item href="/">Transactions List</b-nav-item>
          <b-nav-item href="/new">New Transaction</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </div>
  </b-navbar>
</template>

<script>

</script>

<style scoped>
.app-bar {
  margin-bottom: 30px;
  padding: 15px 0;
  box-shadow: 0 0 10px gray;
  border-bottom: 1px solid white;
}

.app-bar .title {
  font-size: 2rem;
}

.app-bar a {
  font-size: 1.5rem;
}
</style>