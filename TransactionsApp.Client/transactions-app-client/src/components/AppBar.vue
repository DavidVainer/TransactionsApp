<template>
  <b-navbar toggleable="md" type="dark" variant="primary" class="app-bar">
    <b-container>
      <b-navbar-brand href="/" class="title d-flex gap-2 align-items-center">
        <img alt="Vue logo" src="../assets/logo.png" class="logo">
        <strong>Transactions App</strong>
      </b-navbar-brand>

      <b-navbar-toggle target="nav-collapse" />

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav class="ml-auto">
          <b-nav-item href="/">Transactions List</b-nav-item>
          <b-nav-item href="/new">New Transaction</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-container>
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
  font-size: 1.5rem;
}

.logo {
  height: 30px;
  width: 30px;
}

.navbar-brand {
  white-space: nowrap;
}

.navbar-nav .nav-item .nav-link {
  font-size: 1rem;
  color: white;
}
</style>