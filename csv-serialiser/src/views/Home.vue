<template>
  <v-main>
    <div class="home">
      <div class="row">
        <Search msg="CSV Serialiser"/>
      </div>
    </div>
  </v-main>
</template>

<script>
/*
 * Change template accordingly. This is supposed to receive Data from Search and Converter.
 * Might be a good idea to put a div here and then clean Search and Convert.
 */

import Search from '@/components/Search.vue';

export default {
  props: {
    searchResult: Object,
  },
  name: 'Home',
  components: {
    Search,
  },
  data() {
    return {
      bla: '',
    };
  },
};
</script>
