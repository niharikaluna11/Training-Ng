<template>
    <div class="main">
        <div class="dashboard-header">
            <h2>Update Customer Details</h2>
            <button class="add-button" @click="submitForm">Update</button>
        </div>

        <form @submit.prevent="submitForm" class="customer-form">
            <div class="form-group">
                <label for="accountNumber">Account Number:</label>
                <input type="text" v-model="newCustomer.accountNumber" id="accountNumber" required />
            </div>

            <div class="form-group">
                <label for="accountType">Account Type:</label>
                <select v-model="newCustomer.accountType" id="accountType" required>
                    <option value="1">Checking</option>
                    <option value="2">Savings</option>
                    <option value="3">FixedDeposit</option>
                </select>
            </div>

            <div class="form-group">
                <label for="firstName">First Name:</label>
                <input type="text" v-model="newCustomer.firstName" id="firstName" required />
            </div>

            <div class="form-group">
                <label for="lastName">Last Name:</label>
                <input type="text" v-model="newCustomer.lastName" id="lastName" required />
            </div>

            <div class="form-group">
                <label for="address">Address:</label>
                <textarea v-model="newCustomer.address" id="address" required></textarea>
            </div>

            <div class="form-group">
                <label for="email">Email:</label>
                <input type="email" v-model="newCustomer.email" id="email" required />
            </div>

            <div class="form-group">
                <label for="phoneNumber">Phone Number:</label>
                <input type="text" v-model="newCustomer.phoneNumber" id="phoneNumber" required />
            </div>
        </form>
    </div>
</template>


<script>
import { UpdateCustomer, ViewCustomer } from '@/script/CustomerService';

export default {
    name: "UpdateCustomer",
    data() {
        return {
            customerId: this.$route.query.customerId,
            newCustomer: {
                accountNumber: "",
                accountType: "",
                firstName: "",
                lastName: "",
                address: "",
                email: "",
                phoneNumber: "",
            },
        };
    },
    async mounted() {
        console.log("Customer ID from query params:", this.customerId);

        // Fetch the customer details and populate the form
        try {
            const customerData = await ViewCustomer(this.customerId);

            console.log(customerData);
            const customerindata = customerData.data;
            // Fill form fields with the fetched customer data
            this.newCustomer.accountNumber = customerindata.accountNumber;
            this.newCustomer.accountType = customerindata.accountType;
            this.newCustomer.firstName = customerindata.firstName;
            this.newCustomer.lastName = customerindata.lastName;
            this.newCustomer.address = customerindata.address;
            this.newCustomer.email = customerindata.email;
            this.newCustomer.phoneNumber = customerindata.phoneNumber;

        } catch (error) {
            console.error("Error fetching customer details:", error);
            alert("Error fetching customer details.");
        }
    },
    methods: {
        async submitForm() {
            if (!this.newCustomer.accountType || isNaN(+this.newCustomer.accountType)) {
                alert("Please select a valid account type.");
                return;
            }

            try {
                const res = await UpdateCustomer(
                    this.customerId,
                    this.newCustomer.accountNumber,
                    +this.newCustomer.accountType,
                    this.newCustomer.firstName,
                    this.newCustomer.lastName,
                    this.newCustomer.email,
                    this.newCustomer.phoneNumber,
                    this.newCustomer.address
                );

                if (res.success) {
                    alert(res.message);
                    this.$router.push({ path: '/' });
                } else {
                    alert("Error: " + res.message);
                }
            } catch (error) {
                console.error("Error updating customer:", error);
                alert("There was an error updating the customer.");
            }
        },
    },
};
</script>


<style scoped>
.main {
    width: 80%;
    margin: 0 auto;
    font-family: Arial, sans-serif;
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.dashboard-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}

.dashboard-header h2 {
    font-size: 24px;
    font-weight: bold;
    color: #333;
}

.add-button {
    padding: 10px 20px;
    background-color: var(--c-text-action);
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 16px;
}

.add-button:hover {
    background-color: var(--c-text-secondary);
}

.customer-form {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
}

.form-group {
    display: flex;
    flex-direction: column;
}

.form-group label {
    font-size: 14px;
    margin-bottom: 5px;
    color: #333;
}

.form-group input,
.form-group select,
.form-group textarea {
    padding: 10px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 4px;
    margin-top: 5px;
    width: 100%;
}

.form-group textarea {
    resize: vertical;
    height: 100px;
}

.form-group input[type="text"],
.form-group input[type="email"] {
    height: 36px;
}

.form-group select {
    height: 36px;
}

.form-group button {
    padding: 10px 20px;
    background-color: #28a745;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.form-group button:hover {
    background-color: var(--c-text-action);
}

/* Make the form responsive for smaller screens */
@media (max-width: 768px) {
    .customer-form {
        grid-template-columns: 1fr;
    }
}
</style>