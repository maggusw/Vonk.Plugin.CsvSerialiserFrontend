<script>
import axios from 'axios';

const apiClient = axios.create({
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  },
});

export default {
  async getSearchResult(url) {
    try {
      const response = await apiClient.get(url);
      return response.data;
    } catch (err) {
      return err;
    }
  },
};
</script>
