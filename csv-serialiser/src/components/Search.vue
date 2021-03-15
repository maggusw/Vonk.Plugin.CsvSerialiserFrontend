<template>
  <v-main>
    <div class="hello" align="center">
      <h1>{{ msg }}</h1>
      <v-form>
        <v-container class="grey lighten-3 mb-6">
          <v-row>
            <v-col>
              <v-text-field label="Base URL" :value="baseUrl" disabled single-line></v-text-field>
            </v-col>
            <v-col>
              <v-text-field label="Query" type="text"
                            @keydown.enter="submit" single-line></v-text-field>
            </v-col>
            <v-col>
              <v-text-field label="Plugin" :value="appendUrl" disabled single-line></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-form>
      <v-progress-circular
        v-if="loading"
        indeterminate
        size="50">{{ elapsedTime }}s</v-progress-circular>

      <v-alert
        v-if="errMessage"
        dense
        type="error"
      >{{errMessage}}</v-alert>

      <v-container v-if="set" fluid>
        This is a preview of 50 entries.
        There are {{convertedCsv.objects.length}} entries in total.
        <v-card class="m-auto" max-width="75%" max-height="70%">
          <v-select
            v-model="value"
            :items="convertedCsv.header"
            label="Select columns"
            multiple
            return-object>
            <template v-slot:selection="{ item, index }">
              <v-chip v-if="index === 0">
                <span>{{ item.text }}</span>
              </v-chip>
              <span
                v-if="index === 1"
                class="grey--text caption"
              >(+{{ value.length - 1 }} others)</span>
            </template>
          </v-select>
          <v-data-table
            dense
            height="300px"
            class="elevation-1"
            :items-per-page="50"
            disable-pagination
            :headers="selectedHeaders"
            :items="convertedCsv.objects.slice(0, 50)">
          </v-data-table>
          <v-select
            label="Choose from available delimiters"
            v-model="delim"
            :items="delims"
          ></v-select>
          <v-btn @click="generateCsv(selectedHeaders, convertedCsv, delim)">
            Export
          </v-btn>
        </v-card>
      </v-container>
    </div>
  </v-main>
</template>
<script>

/*
 * This is the search component.
 * Supposed to deliver data from VonkSearchService.
 */

import VonkSearchService from '@/services/VonkSearchService.vue';
import CsvParser from '@/services/CsvParserService.vue';
import CsvConverterService from '@/services/CsvConverterService.vue';

export default {
  name: 'Search',
  props: {
    msg: String,
  },
  data() {
    return {
      value: [],
      delim: '',
      url: '',
      response: '',
      baseUrl: 'http://localhost:4080/R4/',
      // baseUrl: 'https://hdp-fhir-dev-pub.charite.de/',
      appendUrl: '/$csv',
      convertedCsv: Object,
      selectedHeaders: [],
      csv: String,
      loading: false,
      elapsedTime: 0,
      set: false,
      delims: [',', ';', '|'],
      errMessage: '',
    };
  },
  methods: {
    async submit(event) {
      if (this.errMessage !== '') this.errMessage = '';
      if (event.target.value === '') {
        this.errMessage = 'Please specify a query';
      } else {
        this.url = event.target.value;
        if (this.response !== '') await this.resetData();
        this.response = await VonkSearchService
          .getSearchResult(this.baseUrl + this.url + this.appendUrl);
        if (this.response.stack && this.response.message) {
          this.errMessage = this.response.message;
        } else {
          this.convertedCsv = await CsvParser.convertCsv(this.response);
          this.selectedHeaders = await this.convertedCsv.header;
          if (typeof this.convertedCsv !== 'undefined') this.set = true;
        }
      }
    },
    async resetData() {
      this.response = '';
      this.convertedCsv = undefined;
      this.selectedHeaders = [];
      this.set = false;
      this.elapsedTime = 0;
    },
    generateCsv(selectedHeaders, convertedCsv, delim) {
      this.csv = CsvConverterService
        .convertTableToCsv(selectedHeaders, convertedCsv.objects, delim);
    },
    setTimer() {
      setInterval(() => {
        this.elapsedTime += 1; // needs resetting
      }, 1000);
    },
    getDelims() {
      if (this.set) this.delims = CsvParser.checkDelims(this.convertedCsv.objects, this.delims);
    },
  },
  watch: {
    value(val) {
      this.selectedHeaders = val;
    },
    url() {
      this.loading = true;
      this.setTimer();
    },
    response() {
      this.loading = false;
    },
    delim(val) {
      this.delim = val;
    },
  },
};
</script>

<style>
pre {
  height: auto;
  max-height: 300px;
  overflow: auto;
  background-color: #eeeeee;
  word-break: normal !important;
  word-wrap: normal !important;
  white-space: pre !important;
}
</style>
