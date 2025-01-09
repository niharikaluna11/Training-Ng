<template>

    <main class="main">
        <div class="dashboard-header">
            <h2>Bank Dashboard</h2>
            <router-link to="/AddCustomerPage" class="nav-link active text-light" aria-current="page">
                <button class="add-button">ADD CUSTOMER</button>
            </router-link>



            <!-- Search Section -->
            <div class="search-section">
                <input type="text" placeholder="Search by First Name" v-model="search.firstName"
                    @input="searchByFirstName" />
                <input type="text" placeholder="Search by Last Name" v-model="search.lastName"
                    @input="searchByLastName" />
                <input type="text" placeholder="Search by Account Number" v-model="search.accountNumber"
                    @input="searchByAccountNumber" />
                <input type="text" placeholder="Search by Phone Number" v-model="search.phoneNumber"
                    @input="searchByPhoneNumber" />
            </div>

        </div>
        <div class="content">
            <table cellspacing="2" cellpadding="10">
                <thead>
                    <tr>
                        <th>Account Number</th>
                        <th>Account Type</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Address</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Options</th>
                        <!--Delete veiw and update-->
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="customer in customers" :key="customer.accountNumber">
                        <td>{{ customer.accountNumber }}</td>
                        <td>{{ customer.accountType }}</td>
                        <td>{{ customer.firstName }}</td>
                        <td>{{ customer.lastName }}</td>
                        <td>{{ customer.address }}</td>
                        <td>{{ customer.email }}</td>
                        <td>{{ customer.phoneNumber }}</td>
                        <td>
                            <button @click="viewCustomer(customer.customerId)">View</button>

                            <router-link
                                :to="{ path: '/UpdateCustomerPage', query: { customerId: customer.customerId } }"
                                class="nav-link active text-light" aria-current="page">
                                <button>Update</button>
                            </router-link>

                            <button @click="deleteCustomer(customer.customerId)">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>

            <div v-if="selectedCustomer" class="modal-overlay" @click="closeModal">
                <div class="modal" @click.stop>
                    <h3>Customer Details</h3>
                    <p><strong>Account Number:</strong> {{ selectedCustomer.accountNumber }}</p>
                    <p><strong>Account Type:</strong> {{ selectedCustomer.accountType }}</p>
                    <p><strong>First Name:</strong> {{ selectedCustomer.firstName }}</p>
                    <p><strong>Last Name:</strong> {{ selectedCustomer.lastName }}</p>
                    <p><strong>Address:</strong> {{ selectedCustomer.address }}</p>
                    <p><strong>Email:</strong> {{ selectedCustomer.email }}</p>
                    <p><strong>Phone:</strong> {{ selectedCustomer.phoneNumber }}</p>
                    <button @click="closeModal">Close</button>
                </div>
            </div>

        </div>
    </main>

</template>



<script>
import { DeleteCustomer, Display, GetCustomerByAccountNumber, GetCustomerByFirstName, GetCustomerByLastName, GetCustomerByPhoneNumber, ViewCustomer } from '@/script/CustomerService';
import { setToken } from '@/script/myAxiosInterceptor';

// wrong auth token
// const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJSb2xlcyI6IlRlbGxlciIsImV4cCI6MTczNjMzMjgwMH0.9RGZH6vlaKInoWV-K2o9qS7b_lMiWnlPb6v2VEn_p3Y"; no auth

// teller
// const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJSb2xlcyI6IlRlbGxlciIsImV4cCI6MTczNjMzMjgwMH0.Z9RJl61ZNuAVnv2plrGwDbQ8alBMorbg4RoAOjWJ1CI"; // teller

//zonemanager branchmanager
const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJSb2xlcyI6IlpvbmVNYW5hZ2VyIiwiZXhwIjoxNzM2MzMyODAwfQ.1wqVqpeW9zFg_yXCwU8-WC71hcqs0Cs-8N8ZGTGqbGE"; //zonemanager branchmanager

const name = "Niharikaa";
const role = "BranchManager"
//ZoneManager,Teller,BranchManager
setToken(token, name, role);


export default {
    name: "HomePage",
    data() {
        return {
            customers: [],
            selectedCustomer: null,
            search: {
                firstName: "",
                lastName: "",
                accountNumber: "",
                phoneNumber: "",
            },
        };
    },
    mounted() {
        this.fetchCustomers();
    },
    methods: {
        async fetchCustomers() {
            try {
                const data = await Display();
                this.customers = data;
            } catch (error) {
                console.error("Error fetching customer data:", error);
            }
        },
        async searchByFirstName() {
            try {
                if (this.search.firstName.trim() === "") {
                    this.fetchCustomers(); // Reset to full list
                    return;
                }
                const data = await GetCustomerByFirstName(this.search.firstName);
                this.customers = data;
            } catch (error) {
                console.error("Error searching by first name:", error);
            }
        },
        async searchByLastName() {
            try {
                if (this.search.lastName.trim() === "") {
                    this.fetchCustomers(); // Reset to full list
                    return;
                }
                const data = await GetCustomerByLastName(this.search.lastName);
                this.customers = data;
            } catch (error) {
                console.error("Error searching by last name:", error);
            }
        },
        async searchByAccountNumber() {
            try {
                if (this.search.accountNumber.trim() === "") {
                    this.fetchCustomers(); // Reset to full list
                    return;
                }
                const data = await GetCustomerByAccountNumber(this.search.accountNumber);
                this.customers = data;
            } catch (error) {
                console.error("Error searching by account number:", error);
            }
        },
        async searchByPhoneNumber() {
            try {
                if (this.search.phoneNumber.trim() === "") {
                    this.fetchCustomers(); // Reset to full list
                    return;
                }
                const data = await GetCustomerByPhoneNumber(this.search.phoneNumber);
                this.customers = data;
            } catch (error) {
                console.error("Error searching by phone number:", error);
            }
        },
        async viewCustomer(customerId) {
            try {
                const customerDetails = await ViewCustomer(customerId);
                this.selectedCustomer = customerDetails.data;
            } catch (error) {
                console.error("Error fetching customer details:", error);
                alert("Failed to fetch customer details. Please try again later.");
            }
        },
        closeModal() {
            this.selectedCustomer = null;
        },
        async deleteCustomer(customerId) {
            const confirmDelete = confirm("Are you sure you want to delete this customer?");
            if (confirmDelete) {
                try {
                    const res = await DeleteCustomer(customerId);
                    this.fetchCustomers();
                    alert(res.message);
                } catch (error) {
                    console.error("Error deleting customer:", error);
                    alert("Failed to delete customer. You are unauthorized to do so.");
                }
            }
        }


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

.search-section {
    display: flex;
    justify-content: space-between;
    gap: 10px;
    margin-top: 10px;
}

.search-section input {
    padding: 8px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 4px;
    width: 20%;
}

.content {
    margin-top: 20px;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

table th,
table td {
    padding: 10px;
    text-align: left;
    border: 1px solid #ddd;
}

table th {
    background-color: #f1f1f1;
    font-weight: bold;
}


button {
    padding: 5px 10px;
    background-color: var(--c-text-action);
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 14px;
    margin-right: 5px;
}

button:hover {
    background-color: var(--c-text-secondary);
}

button:focus {
    outline: none;
}

button:active {
    background-color: var(--c-text-secondary);
}

/* Modal styles */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.modal {
    background-color: #fff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
    width: 400px;
    text-align: left;
}

.modal h3 {
    font-size: 20px;
    margin-bottom: 15px;
    font-weight: bold;
    color: #333;
}

.modal p {
    font-size: 14px;
    color: #555;
    margin-bottom: 10px;
}

.modal button {
    padding: 10px 15px;
    background-color: var(--c-text-action);
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 14px;
}

.modal button:hover {
    background-color: var(--c-text-secondary);
}

/* Make the layout responsive for smaller screens */
@media (max-width: 768px) {
    .main {
        width: 90%;
    }

    .search-section {
        flex-direction: column;
    }

    .search-section input {
        width: 100%;
        margin-bottom: 10px;
    }

    table {
        font-size: 12px;
    }

    table th,
    table td {
        padding: 8px;
    }
}
</style>