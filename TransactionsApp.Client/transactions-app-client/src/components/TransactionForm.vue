<template>
  <div>
    <h2>{{ isEditMode ? "Edit Transaction" : "New Transaction" }}</h2>
    <b-form @submit.prevent="handleSubmit" class="d-flex flex-column gap-3">
      <!-- Full Name Hebrew -->
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
          Full Name (Hebrew) must contain only Hebrew letters, apostrophes, dashes, or spaces and be up to 20 characters.
        </div>
      </b-form-group>

      <!-- Full Name English -->
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
          Full Name (English) must contain only English letters, apostrophes, dashes, or spaces and be up to 15 characters.
        </div>
      </b-form-group>

      <!-- Birth Date -->
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
          Please select a valid date.
        </div>
      </b-form-group>

      <!-- User Identity -->
      <b-form-group label="Identity:" label-for="identity">
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
          Identity must be exactly 9 digits.
        </div>
      </b-form-group>

      <!-- Transaction Type -->
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
          Please select a transaction type.
        </div>
      </b-form-group>

      <!-- Amount -->
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
          Amount must be a numeric value up to 10 digits.
        </div>
      </b-form-group>

      <!-- Account Number -->
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
          Account Number must be a numeric value up to 10 digits.
        </div>
      </b-form-group>

      <!-- Submit Button -->
      <b-button type="submit" variant="primary" :disabled="!isFormValid">
        {{ isEditMode ? "Save Changes" : "Submit" }}
      </b-button>
    </b-form>

    <!-- Message Feedback -->
    <div v-if="message" :class="`alert ${isError ? 'alert-danger' : 'alert-success'}`" role="alert" class="mt-3">
      {{ message }}
    </div>
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
        userIdentity: user.userIdentity,
        dateOfBirth: this.formatDate(user.dateOfBirth),
        amount,
        accountNumber,
      };
    }
  },
  data() {
    return {
      form: {
        transactionId: "",
        userIdentity: "",
        fullNameHebrew: "",
        fullNameEnglish: "",
        dateOfBirth: "",
        transactionType: null,
        amount: 0,
        accountNumber: "",
      },
      touched: {
        fullNameHebrew: false,
        fullNameEnglish: false,
        userIdentity: false,
        dateOfBirth: false,
        transactionType: false,
        amount: false,
        accountNumber: false,
      },
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
    // Custom Validation Rules
    isValidFullNameHebrew() {
      return /^[א-ת'\-\s]{1,20}$/.test(this.form.fullNameHebrew);
    },
    isValidFullNameEnglish() {
      return /^[A-Za-z'\-\s]{1,15}$/.test(this.form.fullNameEnglish);
    },
    isValidUserIdentity() {
      return /^\d{9}$/.test(this.form.userIdentity);
    },
    isValidDateOfBirth() {
      return !!this.form.dateOfBirth && !isNaN(Date.parse(this.form.dateOfBirth));
    },
    isValidAmount() {
      return /^\d{1,10}$/.test(this.form.amount.toString());
    },
    isValidAccountNumber() {
      return /^\d{1,10}$/.test(this.form.accountNumber);
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
    markTouched(field) {
      this.touched[field] = true;
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      return date.toISOString().split("T")[0];
    },
    async handleSubmit() {
      this.isFormSubmitted = true;

      if (!this.isFormValid) {
        this.message = "Please fix the errors in the form.";
        this.isError = true;
        return;
      }

      try {
        if (this.isEditMode) {
          await this.updateTransaction(this.form);
          this.message = "Transaction updated successfully!";
        } else {
          await this.createTransaction(this.form);
          this.message = "Transaction created successfully!";
          this.isError = false;
          this.resetForm();
        }
      } catch (error) {
        this.message = "An error occurred while processing the transaction. Please try again!";
        this.isError = true;
      }
    },
    resetForm() {
      this.form = {
        transactionId: "",
        userIdentity: "",
        fullNameHebrew: "",
        fullNameEnglish: "",
        dateOfBirth: "",
        transactionType: null,
        amount: 0,
        accountNumber: "",
      };
      this.touched = {
        fullNameHebrew: false,
        fullNameEnglish: false,
        userIdentity: false,
        dateOfBirth: false,
        transactionType: false,
        amount: false,
        accountNumber: false,
      };
      this.isFormSubmitted = false;
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
