<template>
  <div>
    <h2>Transactions List</h2>
    <b-table :items="transactions" :fields="fields">
      <template #cell(actions)="row">
        <div class="d-flex gap-1">
          <b-button size="sm" variant="secondary" @click="editTransaction(row.item)" title="Edit">
            <BIconPencilFill />
          </b-button>
          <b-button size="sm" variant="danger" @click="removeTransaction(row.item.id)" title="Delete">
            <BIconTrashFill />
          </b-button>
        </div>
      </template>
    </b-table>
  </div>
</template>

<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import { BIconTrashFill, BIconPencilFill } from 'bootstrap-vue'

export default {
  components: {
    BIconPencilFill,
    BIconTrashFill,
  },
  data() {
    return {
      fields: [
        { key: "user.fullNameHebrew", label: "Full Name (Hebrew)" },
        { key: "user.fullNameEnglish", label: "Full Name (English)" },
        { key: "user.dateOfBirth", label: "Date of Birth", formatter: (value) => new Date(value).toLocaleString() },
        { key: "user.userId", label: "Identity" },
        { key: "transactionType", label: "Type" },
        { key: "amount", label: "Amount" },
        { key: "accountNumber", label: "Account Number" },
        { key: "date", label: "Date", formatter: (value) => new Date(value).toLocaleString() },
        { key: "status", label: "Status" },
        { key: "actions", label: "Actions" },
      ],
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
