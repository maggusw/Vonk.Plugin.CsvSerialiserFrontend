<template>
  <v-app>
    <div id="nav">
      <div style="text-align: center;">
        <router-link to="/">Home</router-link> |
        <router-link to="/about">About</router-link>
      </div>
      <router-view></router-view>
    </div>
  </v-app>
</template>
