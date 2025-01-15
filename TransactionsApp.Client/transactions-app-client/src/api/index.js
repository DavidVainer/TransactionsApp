import axios from 'axios';

const instance = axios.create({
   baseURL: process.env.VUE_APP_API_BASE_URL,
    headers: {
       'Content-Type': 'application/json',
    }
});

export const fetchTransactionsAsync = async () => {
    const response = await instance.get("/transactions");
    return response.data;
};

export const createTransactionAsync = async (payload) => {
    const response = await instance.post("/transactions", payload);
    return response.data;
};

export const updateTransactionAsync = async (payload) => {
    const response = await instance.put(`/transactions/${payload.transactionId}`, payload);
    return response.data;
};

export const deleteTransactionAsync = async (transactionId) => {
    await instance.delete(`/transactions/${transactionId}`);
    return transactionId;
};
