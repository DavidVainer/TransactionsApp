<template>
  <div>
    <h2>Transactions List</h2>
    <b-table :items="transactions" :fields="fields">
      <template #cell(actions)="row">
        <div class="d-flex gap-1">
          <b-button size="sm" variant="secondary" @click="editTransaction(row.item)" title="Edit">
            <BIconPencilFill />
          </b-button>
          <b-button size="sm" variant="danger" @click="removeTransaction(row.item.id)" title="Cancel">
            <BIconXCircleFill />
          </b-button>
        </div>
      </template>
    </b-table>
  </div>
</template>

<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import { BIconXCircleFill, BIconPencilFill } from 'bootstrap-vue'

export default {
  components: {
    BIconPencilFill,
    BIconXCircleFill,
  },
  data() {
    return {
      fields: [
        { key: "user.userIdentity", label: "User Identity" },
        { key: "transactionType", label: "Transaction Type", formatter: (value) => this.transactionTypeLabels[value] },
        { key: "amount", label: "Amount" },
        { key: "accountNumber", label: "Account Number" },
        { key: "date", label: "Date", formatter: (value) => new Date(value).toLocaleString() },
        { key: "status", label: "Status", formatter: (value) => this.statusLabels[value] },
        { key: "actions", label: "Actions" },
      ],
      transactionTypeLabels: {
        1: "Deposit",
        2: "Withdrawal",
      },
      statusLabels: {
        1: "Pending",
        2: "Completed",
        3: "Failed",
        4: "Canceled",
      }
    };
  },
  created() {
    this.fetchTransactions();
  },
  computed: {
    ...mapGetters(["transactions"]),
  },
  methods: {
    ...mapActions(["fetchTransactions", "deleteTransaction"]),
    ...mapMutations(["setTransactionToEdit"]),
    editTransaction(transaction) {
      this.setTransactionToEdit(transaction);
      this.$router.push("/new");
    },
    removeTransaction(transactionId) {
      if (confirm("Are you sure you want to delete this transaction?")) {
        this.deleteTransaction(transactionId);
      }
    },
  },
};
</script>
