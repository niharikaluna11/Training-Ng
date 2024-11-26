<template>

    <header class="header">
        <div class="header-content responsive-wrapper">
            <div class="header-logo">
                <a href="#" style="text-decoration: none;">
                    <div>
                        <img src="https://assets.codepen.io/285131/untitled-ui-icon.svg" />
                    </div>
                 <p style="color: black;">TicketSolve</p>

                    <!-- <img src="https://assets.codepen.io/285131/untitled-ui.svg" /> -->
                </a>
            </div>
            <div class="header-navigation">
                <nav class="header-navigation-links">
                    <router-link to="/HomePage" class="nav-link active text-light" aria-current="page" style=" margin-bottom: 9px;">
                        <a href="#">Home </a>
                    </router-link>
                    <!-- <a href="#About"> About </a>
                    <a href="#Feature"> Features </a>
                    <a href="#learn-more"> Contact us </a> -->
                </nav>
                <div class="header-navigation-actions">

                    <router-link to="/Login" class="nav-link active text-light signup" aria-current="page" >
                        <a href="#" class="button">
                            <i class="ph-lightning-bold"></i>
                            <span>Sign In</span>
                        </a>
                    </router-link>
                    <a href="#" class="icon-button">
                        <i class="ph-gear-bold"></i>
                    </a>
                    <a href="#" class="icon-button">
                        <i class="ph-bell-bold"></i>
                    </a>
                    <a href="#" class="avatar">
                        <img src="@/Images/profilepicimg.jpg" alt="profile">
                    </a>
                </div>
            </div>
            <a href="#" class="button">
                <i class="ph-list-bold"></i>
                <span>Menu</span>
            </a>
        </div>
    </header>

</template>

<script>
export default {
    name: 'BaseHeader',
};
</script>

<style scoped>
p{
    margin-bottom: 9px;
    text-decoration: none;
    
}
/* Add your header styles here */
.header {
    padding-top: 50px;
}

.header-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.header-logo img {
    max-height: 50px;
}

</style>
