<script>
export default {
    name: 'UserSideBar',
    methods: {
        logout() {
            // Clear localStorage and sessionStorage
            localStorage.clear();
            sessionStorage.clear();

            this.$router.push('/');
        }
    }
};
</script>



<template>
    <aside :class="{ 'is-expanded': is_expanded }">

        <div class="menu-toggle-wrap">
            <button class="menu-toggle" @click="ToggleMenu">
                <span class="material-icons">
                    <!-- keyboard_double_arrow_right -->
                    <img src="@/Images/right-keyboard.png" alt="profile"
                        style="height: 40px;width: 40px;padding-top: 15px;">
                </span>
            </button>
        </div>

        <h3></h3>
        <div class="menu">


            <router-link class="button" to="/UserDashboard">
                <span class="material-icons"></span>
                <span class="text">User Rights</span>
            </router-link>

            <router-link class="button" to="/" @click="logout">
                <span class="material-icons"></span>
                <span class="text">Log Out</span>
            </router-link>

        </div>
    </aside>
</template>

<script setup>
import { ref } from 'vue';
// State to track sidebar expansion
const is_expanded = ref(false);

// Toggle menu expansion
const ToggleMenu = () => {
    is_expanded.value = !is_expanded.value;

};


</script>


<style lang="scss" scoped>
aside {
    display: flex;
    flex-direction: column;
    // width: calc(4rem + 3rem);
    overflow: hidden;
    min-height: 100vh;
    //padding: 1rem;


    color: var(--light);

    transition: width 0.2s ease-out;

    .flex {
        flex: 1;
    }

    button {
        outline: none;
        background: transparent;
        border: none;
        cursor: pointer;
    }

    .logo {
        margin-bottom: 1rem;

        img {
            padding-top: 15px;
            width: 0.5rem;
        }
    }

    .menu-toggle-wrap {
        display: flex;
        justify-content: flex-end;

        &.is-expanded {
            opacity: 1;
            visibility: visible;
            transition: opacity 0.2s ease-out, visibility 0s;
        }

        .menu-toggle {
            transition: transform 0.2s ease-out;

            .material-icons {
                font-size: 2rem;
                color: var(--light);
            }

            &:hover .material-icons {
                color: aliceblue;
                transform: translateX(0.5rem);
            }
        }

    }


    h3,
    .button .text {
        opacity: 0;
        transition: opacity 0.3s ease-out;
    }

    h3 {
        color: var(--gray);
        font-size: 12px;
        margin-bottom: 0.5rem;
        text-transform: uppercase;
    }

    .menu {
        margin: 0 -1rem;
        font-size: small;

        .button {
            display: flex;
            align-items: center;
            text-decoration: none;
            margin: 0px;
            border: 0px;
            border-radius: 0px;
            /* padding: 0.5rem 0.5rem; */
            transition: 0.2s ease-out;

            .material-icons {

                color: var(--light);
                margin-right: 1rem;
            }

            .text {
                color: var(--light);
            }

            &:hover,
            &.router-link-active {


                .material-icons,
                .text {
                    color: var(--c-text-secondary);
                }
            }


        }
    }

    &.is-expanded {
        width: 200px;

        .menu-toggle-wrap {
            .menu-toggle {
                transform: rotate(-180deg);
            }
        }

        h3,
        .button .text {
            opacity: 1;
        }
    }

    @media (max-width: 768px) {
        position: fixed;
        z-index: 99;
    }
}
</style>
