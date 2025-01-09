import axios from "axios";

const BASE_URL = "https://localhost:7013/";

export const Display = async () => {
    const url = BASE_URL + "api/Customer/GetAllCustomer";
    try {

        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("token not found.");
        }
        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching dashboard summary:", err);
        throw err;
    }
};

export const ViewCustomer = async (customerId) => {
    const url = `${BASE_URL}api/Customer/GetCustomerByID?customerId=${customerId}`;
    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching customer details:", err);
        throw err;
    }
};


export const UpdateCustomer = async (
    customerId,
    accountNumber,
    accountType,
    firstName,
    lastName,
    email,
    phoneNumber,
    address
) => {
    const url = `${BASE_URL}api/Customer/UpdateCustomer?id=${customerId}`;

    const customerData = new FormData();
    customerData.append("AccountNumber", accountNumber);
    customerData.append("AccountType", +accountType);
    customerData.append("FirstName", firstName);
    customerData.append("LastName", lastName);
    customerData.append("Address", address);
    customerData.append("Email", email);
    customerData.append("PhoneNumber", phoneNumber);

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.put(url, customerData, {
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'multipart/form-data',
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error updating customer details:", err);
        throw err;
    }
};




export const DeleteCustomer = async (customerId) => {
    const url = `${BASE_URL}api/Customer/DeleteCustomer?id=${customerId}`;
    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.delete(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error deleting customer:", err);
        throw err;
    }
};



export const CreateCustomer = async (accountNumber, accountType,
    firstName, lastName, address,
    email, phoneNumber) => {
    const url = `${BASE_URL}api/Customer/CreateCustomer`;

    const customerData = new FormData();
    customerData.append("AccountNumber", accountNumber);
    customerData.append("AccountType", +accountType);
    customerData.append("FirstName", firstName);
    customerData.append("LastName", lastName);
    customerData.append("Address", address);
    customerData.append("Email", email);
    customerData.append("PhoneNumber", phoneNumber);

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.post(url, customerData, {
            headers: {
                Authorization: `Bearer ${token}`, 
                'Content-Type': 'multipart/form-data', 
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error creating customer:", err);
        throw err;
    }
};



export const GetCustomerByFirstName = async (firstName) => {
    const url = `${BASE_URL}api/Customer/GetBYfirstName?firstName=${firstName}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching customer by first name:", err);
        throw err;
    }
};

export const GetCustomerByLastName = async (lastName) => {
    const url = `${BASE_URL}api/Customer/GetBYlastName?lastName=${lastName}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching customer by last name:", err);
        throw err;
    }
};

export const GetCustomerByAccountNumber = async (accountNumber) => {
    const url = `${BASE_URL}api/Customer/GetBYaccountNumber?accountNumber=${accountNumber}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching customer by account number:", err);
        throw err;
    }
};
export const GetCustomerByPhoneNumber = async (phoneNumber) => {
    const url = `${BASE_URL}api/Customer/GetBYphoneNumber?phoneNumber=${phoneNumber}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        console.log("Response:", response.data);
        return response.data;
    } catch (err) {
        console.error("Error fetching customer by phone number:", err);
        throw err;
    }
};
