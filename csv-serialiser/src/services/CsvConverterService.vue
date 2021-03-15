<script>
let csv = '';

export default {
  convertTableToCsv(selectedHeaders, values, delim) {
    const headerLine = selectedHeaders.map((item) => item.value);
    csv = headerLine.join(delim);
    csv += '\r\n';

    for (let i = 0; i < values.length; i += 1) {
      selectedHeaders.forEach((item, index) => {
        if (values[i][item.value] === undefined) {
          return;
        }
        if (index !== selectedHeaders.length - 1) {
          this.addValue(values[i][item.value], delim);
        } else {
          this.addValue(values[i][item.value], '');
        }
      });
    }
    return this.createDownload();
  },
  addValue(value, delim) {
    if (delim === '') {
      csv += `${value}\r\n`;
    } else {
      csv += value + delim;
    }
  },
  createDownload() {
    const anchor = document.createElement('a');
    anchor.href = `data:text/csv;charset=utf-8,${encodeURIComponent(csv)}`;
    anchor.target = '_blank';
    anchor.download = 'data.csv';
    anchor.click();
  },
};
</script>
