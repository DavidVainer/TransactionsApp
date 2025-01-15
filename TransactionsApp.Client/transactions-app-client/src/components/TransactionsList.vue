<template>
  <div>
    <h2>Transactions List</h2>
    <div class="table-wrapper">
      <div v-if="isLoading" class="loader-wrapper">
        <b-spinner variant="primary" style="width: 5rem; height: 5rem"></b-spinner>
      </div>
      <div v-else-if="errorMessage" class="alert alert-danger" role="alert">
        {{ errorMessage }}
      </div>
      <b-table v-else :items="transactions" :fields="fields">
        <template #cell(actions)="row">
          <div class="d-flex gap-1 justify-content-center">
            <b-button
                size="sm"
                variant="secondary"
                @click="editTransaction(row.item)"
                title="Edit"
            >
              <BIconPencilFill />
            </b-button>
            <b-button
                size="sm"
                variant="danger"
                @click="removeTransaction(row.item.id)"
                title="Cancel"
            >
              <BIconXCircleFill />
            </b-button>
          </div>
        </template>
      </b-table>
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import { BIconXCircleFill, BIconPencilFill } from "bootstrap-vue";
import { transactionTypeLabels, statusLabels } from "@/enums";

export default {
  components: {
    BIconPencilFill,
    BIconXCircleFill,
  },
  data() {
    return {
      fields: [
        { key: "user.userIdentity", label: "Identity (Teudat Zehut)" },
        { key: "transactionType", label: "Transaction Type", formatter: (value) => transactionTypeLabels[value] },
        { key: "amount", label: "Amount" },
        { key: "accountNumber", label: "Account Number" },
        { key: "date", label: "Date", formatter: (value) => new Date(value).toLocaleString() },
        { key: "status", label: "Status", formatter: (value) => statusLabels[value] },
        { key: "actions", label: "Actions", class: "text-center" },
      ],
      isLoading: true,
      errorMessage:"",
    };
  },
  created() {
    this.fetchTransactionsList();
  },
  computed: {
    ...mapGetters(["transactions"]),
  },
  methods: {
    ...mapActions(["fetchTransactions", "deleteTransaction"]),
    ...mapMutations(["setTransactionToEdit"]),
    async fetchTransactionsList() {
      try {
        this.isLoading = true;
        await this.fetchTransactions();
      } catch (error) {
        this.errorMessage = `Error fetching transactions list: ${error}`;
      } finally {
        this.isLoading = false;
      }
    },
    editTransaction(transaction) {
      this.setTransactionToEdit(transaction);
      this.$router.push("/new");
    },
    removeTransaction(transactionId) {
      if (confirm("Are you sure you want to permanently cancel this transaction?")) {
        this.deleteTransaction(transactionId);
      }
    },
  },
};
</script>

<style scoped>
.loader-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
  margin-top: 20px;
}

.table-wrapper {
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}
</style>
