export const validateFullNameHebrew = (value) => {
    return value?.trim() && /^[א-ת'\-\s]{1,20}$/.test(value);
};

export const validateFullNameEnglish = (value) => {
    return value?.trim() && /^[A-Za-z'\-\s]{1,15}$/.test(value);
}

export const validateUserIdentity = (value) => {
    return value?.trim() && /^\d{9}$/.test(value);
}

export const validateDateOfBirth = (value) => {
    return !!value && !isNaN(Date.parse(value));
}

export const validateAmount = (value) => {
    return value > 0 && value < 10000000000;
}

export const validateAccountNumber = (value) => {
    return value?.trim() && /^\d{1,10}$/.test(value);
}