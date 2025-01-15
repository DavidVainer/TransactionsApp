// Validates a full name written in Hebrew
// The name must be 1 to 20 characters long and can include Hebrew letters, single quotes, hyphens, and spaces.
export const validateFullNameHebrew = (value) => {
    return value?.trim() && /^[א-ת'\-\s]{1,20}$/.test(value);
};

// Validates a full name written in English
// The name must be 1 to 15 characters long and can include English letters, single quotes, hyphens, and spaces.
export const validateFullNameEnglish = (value) => {
    return value?.trim() && /^[A-Za-z'\-\s]{1,15}$/.test(value);
}

// Validates a user's identity number
// The identity number must be exactly 9 digits long.
export const validateUserIdentity = (value) => {
    return value?.trim() && /^\d{9}$/.test(value);
}

// Validates a date of birth
// The input must be a valid date string that can be parsed into a Date object.
export const validateDateOfBirth = (value) => {
    return !!value && !isNaN(Date.parse(value));
}

// Validates an amount
// The amount must be greater than 0 and less than 10000000000.
export const validateAmount = (value) => {
    return value > 0 && value < 10000000000;
}

// Validates a bank account number
// The account number must be 1 to 10 digits long.
export const validateAccountNumber = (value) => {
    return value?.trim() && /^\d{1,10}$/.test(value);
}