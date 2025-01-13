<template>
  <div>
    <h2>{{ isEditMode ? "Edit Transaction" : "New Transaction" }}</h2>
    <b-form @submit.prevent="handleSubmit" class="d-flex flex-column gap-3">
      <b-form-group
          label="Full Name (Hebrew):"
          label-for="full-name-hebrew"
      >
        <b-form-input
            id="full-name-hebrew"
            v-model="form.fullNameHebrew"
            type="text"
            placeholder="Enter full name (Hebrew)"
            :disabled="isEditMode"
            required
        ></b-form-input>
      </b-form-group>

      <b-form-group
          label="Full Name (English):"
          label-for="full-name-english"
      >
        <b-form-input
            id="full-name-english"
            v-model="form.fullNameEnglish"
            type="text"
            placeholder="Enter full name (English)"
            :disabled="isEditMode"
            required
        ></b-form-input>
      </b-form-group>

      <b-form-group
          label="Birth of Date:"
          label-for="birth-of-date"
      >
        <b-form-input
            id="birth-of-date"
            v-model="form.dateOfBirth"
            type="date"
            placeholder="Enter birth of date"
            :disabled="isEditMode"
            required
        ></b-form-input>
      </b-form-group>

      <b-form-group
          label="Identity:"
          label-for="identity"
      >
        <b-form-input
            id="identity"
            v-model="form.userId"
            type="text"
            placeholder="Enter national ID"
            :disabled="isEditMode"
            required
        ></b-form-input>
      </b-form-group>

      <b-form-group label="Transaction Type:" label-for="transaction-type">
        <b-form-select
            id="transaction-type"
            v-model="form.transactionType"
            :options="transactionTypes"
            :disabled="isEditMode"
            required
        ></b-form-select>
      </b-form-group>

      <b-form-group
          label="Amount:"
          label-for="amount"
      >
        <b-form-input
            id="amount"
            v-model="form.amount"
            type="text"
            placeholder="Enter amount"
            required
        ></b-form-input>
      </b-form-group>

      <b-form-group
          label="Account Number:"
          label-for="account-number"
      >
        <b-form-input
            id="account-number"
            v-model="form.accountNumber"
            type="text"
            placeholder="Enter account number"
            required
        ></b-form-input>
      </b-form-group>

      <b-button type="submit" variant="primary">
        {{ isEditMode ? "Save Changes" : "Submit" }}
      </b-button>
    </b-form>
  </div>
</template>

<script>
import { mapActions, mapGetters, mapMutations } from "vuex";

export default {
  created() {
    if (this.transactionToEdit) {
      const { id, user, transactionType, amount, accountNumber } = this.transactionToEdit;

      this.form = {
        transactionId: id,
        fullNameHebrew: user.fullNameHebrew,
        fullNameEnglish: user.fullNameEnglish,
        transactionType,
        userId: user.userId,
        dateOfBirth: user.dateOfBirth,
        amount,
        accountNumber,
      };
    }
  },
  data() {
    return {
      form: {
        transactionId: "",
        userId: "",
        fullNameHebrew: "",
        fullNameEnglish: "",
        dateOfBirth: "",
        transactionType: null,
        amount: "",
        accountNumber: "",
      },
      transactionTypes: [
        { value: null, text: "Select Transaction Type", disabled: true },
        { value: 0, text: "Deposit" },
        { value: 1, text: "Withdrawal" },
      ],
    };
  },
  computed: {
    ...mapGetters(["transactionToEdit"]),
    ...mapMutations(["setTransactionToEdit"]),
    isEditMode() {
      return !!this.transactionToEdit;
    },
  },
  methods: {
    ...mapActions(["updateTransaction", "createTransaction"]),
    handleSubmit() {
      if (this.isEditMode) {
        this.updateTransaction(this.form);
      } else {
        this.createTransaction(this.form);
      }

      this.resetForm();
    },
    resetForm() {
      this.form = {
        transactionId: "",
        userId: "",
        fullNameHebrew: "",
        fullNameEnglish: "",
        dateOfBirth: "",
        transactionType: null,
        amount: "",
        accountNumber: "",
      };
    },
  },
};
</script>

<style scoped>
#transaction-type {
  display: block;
  width: 100%;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: var(--bs-body-color);
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-color: var(--bs-body-bg);
  background-clip: padding-box;
  border: var(--bs-border-width) solid var(--bs-border-color);
  border-radius: var(--bs-border-radius);
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

#transaction-type:disabled {
  background-color: #e9ecef;
}
</style>
