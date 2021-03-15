<script>
export default {
  /*
   * https://stackoverflow.com/questions/28543821/convert-csv-lines-into-javascript-objects
   *
   *
   */
  async convertCsv(csv) {
    const [headerLine, ...lines] = csv.split('\n');

    const valueSeparator = ';';

    const headers = headerLine.split(valueSeparator);

    const objects = lines
      .map((line) => line
        .split(valueSeparator)
        .reduce((object, value, index) => ({
          ...object,
          [headers[index]]: value.slice(1, -1),
        }),
        {}));

    const header = [];

    for (let i = 0; i < headers.length; i += 1) {
      header[i] = {
        text: headers[i],
        value: headers[i],
      };
    }
    return { header, objects };
  },
};
</script>
