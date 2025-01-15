<template>
  <div>
    <h2>{{ isEditMode ? "Edit Transaction" : "New Transaction" }}</h2>
    <b-form @submit.prevent="handleSubmit" class="d-flex flex-column gap-3">
      <b-form-group label="Full Name (Hebrew):" label-for="full-name-hebrew">
        <b-form-input
            id="full-name-hebrew"
            v-model="form.fullNameHebrew"
            type="text"
            placeholder="Enter full name (Hebrew)"
            :disabled="isEditMode"
            @blur="markTouched('fullNameHebrew')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.fullNameHebrew) && !isValidFullNameHebrew" class="text-danger">
          {{ getErrorMessage("fullNameHebrew") }}
        </div>
      </b-form-group>

      <b-form-group label="Full Name (English):" label-for="full-name-english">
        <b-form-input
            id="full-name-english"
            v-model="form.fullNameEnglish"
            type="text"
            placeholder="Enter full name (English)"
            :disabled="isEditMode"
            @blur="markTouched('fullNameEnglish')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.fullNameEnglish) && !isValidFullNameEnglish" class="text-danger">
          {{ getErrorMessage("fullNameEnglish") }}
        </div>
      </b-form-group>

      <b-form-group label="Birth of Date:" label-for="birth-of-date">
        <b-form-input
            id="birth-of-date"
            v-model="form.dateOfBirth"
            type="date"
            placeholder="Enter birth date"
            :disabled="isEditMode"
            @blur="markTouched('dateOfBirth')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.dateOfBirth) && !isValidDateOfBirth" class="text-danger">
          {{ getErrorMessage("dateOfBirth") }}
        </div>
      </b-form-group>

      <b-form-group label="Identity (Teudat Zehut):" label-for="identity">
        <b-form-input
            id="identity"
            v-model="form.userIdentity"
            type="text"
            placeholder="Enter national ID"
            :disabled="isEditMode"
            @blur="markTouched('userIdentity')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.userIdentity) && !isValidUserIdentity" class="text-danger">
          {{ getErrorMessage("userIdentity") }}
        </div>
      </b-form-group>

      <b-form-group label="Transaction Type:" label-for="transaction-type">
        <b-form-select
            id="transaction-type"
            v-model="form.transactionType"
            :options="transactionTypes"
            :disabled="isEditMode"
            @blur="markTouched('transactionType')"
            required
        ></b-form-select>
        <div v-if="(isFormSubmitted || touched.transactionType) && form.transactionType === null" class="text-danger">
          {{ getErrorMessage("transactionType") }}
        </div>
      </b-form-group>

      <b-form-group label="Amount:" label-for="amount">
        <b-form-input
            id="amount"
            v-model.number="form.amount"
            type="number"
            step="any"
            placeholder="Enter amount"
            @blur="markTouched('amount')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.amount) && !isValidAmount" class="text-danger">
          {{ getErrorMessage("amount") }}
        </div>
      </b-form-group>

      <b-form-group label="Account Number:" label-for="account-number">
        <b-form-input
            id="account-number"
            v-model="form.accountNumber"
            type="text"
            placeholder="Enter account number"
            @blur="markTouched('accountNumber')"
            required
        ></b-form-input>
        <div v-if="(isFormSubmitted || touched.accountNumber) && !isValidAccountNumber" class="text-danger">
          {{ getErrorMessage("accountNumber") }}
        </div>
      </b-form-group>

      <b-button type="submit" variant="primary" :disabled="!isFormValid || isLoading">
        <span v-if="isLoading">
          <b-spinner small></b-spinner> Processing...
        </span>
        <span v-else>{{ isEditMode ? "Save Changes" : "Submit" }}</span>
      </b-button>
    </b-form>

    <div v-if="message" :class="`alert ${isError ? 'alert-danger' : 'alert-success'}`" role="alert" class="mt-3">
      {{ message }}
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import {
  validateFullNameHebrew,
  validateFullNameEnglish,
  validateUserIdentity,
  validateDateOfBirth,
  validateAmount,
  validateAccountNumber } from "@/utils/validators";
import { transactionTypeLabels } from "@/enums";

const initialForm = {
  transactionId: "",
  userIdentity: "",
  fullNameHebrew: "",
  fullNameEnglish: "",
  dateOfBirth: "",
  transactionType: null,
  amount: 0,
  accountNumber: "",
};

const initialTouched = {
  fullNameHebrew: false,
  fullNameEnglish: false,
  userIdentity: false,
  dateOfBirth: false,
  transactionType: false,
  amount: false,
  accountNumber: false,
};

export default {
  created() {
    if (this.transactionToEdit) {
      const { id, user, transactionType, amount, accountNumber } = this.transactionToEdit;

      this.form = {
        transactionId: id,
        fullNameHebrew: user.fullNameHebrew,
        fullNameEnglish: user.fullNameEnglish,
        transactionType,
        userIdentity: user.userIdentity,
        dateOfBirth: this.formatDate(user.dateOfBirth),
        amount,
        accountNumber,
      };
    }
  },
  data() {
    return {
      isLoading: false,
      errorMessages: {
        fullNameHebrew: "Full Name (Hebrew) must contain only Hebrew letters, apostrophes, dashes, or spaces and be up to 20 characters.",
        fullNameEnglish: "Full Name (English) must contain only English letters, apostrophes, dashes, or spaces and be up to 15 characters.",
        userIdentity: "Identity must be exactly 9 digits.",
        dateOfBirth: "Please select a valid date.",
        amount: "Amount must be a numeric value up to 10 digits.",
        accountNumber: "Account Number must be a numeric value up to 10 digits.",
        transactionType: "Please select a transaction type.",
      },
      form: initialForm,
      touched: initialTouched,
      transactionTypes: [
        { value: null, text: "Select Transaction Type", disabled: true },
        { value: 1, text: "Deposit" },
        { value: 2, text: "Withdrawal" },
      ],
      message: "",
      isError: false,
      isFormSubmitted: false,
    };
  },
  computed: {
    ...mapGetters(["transactionToEdit"]),
    isEditMode() {
      return !!this.transactionToEdit;
    },
    isValidFullNameHebrew() {
      return validateFullNameHebrew(this.form.fullNameHebrew);
    },
    isValidFullNameEnglish() {
      return validateFullNameEnglish(this.form.fullNameEnglish);
    },
    isValidUserIdentity() {
      return validateUserIdentity(this.form.userIdentity);
    },
    isValidDateOfBirth() {
      return validateDateOfBirth(this.form.dateOfBirth);
    },
    isValidAmount() {
      return validateAmount(this.form.amount);
    },
    isValidAccountNumber() {
      return validateAccountNumber(this.form.accountNumber);
    },
    isFormValid() {
      return (
          this.isValidFullNameHebrew &&
          this.isValidFullNameEnglish &&
          this.isValidUserIdentity &&
          this.isValidDateOfBirth &&
          this.isValidAmount &&
          this.isValidAccountNumber &&
          this.form.transactionType !== null
      );
    },
  },
  methods: {
    ...mapActions(["updateTransaction", "createTransaction"]),
    getErrorMessage(field) {
      return this.errorMessages[field];
    },
    markTouched(field) {
      this.touched[field] = true;
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      date.setDate(date.getDate() + 1);
      return date.toISOString().split("T")[0];
    },
    async handleSubmit() {
      this.isFormSubmitted = true;
      this.isLoading = true;

      try {
        if (this.isEditMode) {
          await this.updateTransaction(this.form);
          this.message = `Transaction updated successfully! New amount is transaction with amount of ${this.form.amount}.`;
        } else {
          await this.createTransaction(this.form);
          this.message = `Transaction updated successfully! User with identity ${this.form.userIdentity}
            made ${transactionTypeLabels[this.form.transactionType]}
            transaction with amount of ${this.form.amount}.`;
          this.isError = false;
          this.resetForm();
        }
      } catch (error) {
        this.message = "An error occurred while processing the transaction. Please try again!";
        this.isError = true;
      } finally {
        this.isLoading = false;
      }
    },
    resetForm() {
      this.form = initialForm;
      this.touched = initialTouched;
    }
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
  color: var(--bs-body-color);
  background-color: var(--bs-body-bg);
  border: 1px solid var(--bs-border-color);
  border-radius: 0.25rem;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;

  &:disabled {
    background-color: #e9ecef;
  }
}
</style>
