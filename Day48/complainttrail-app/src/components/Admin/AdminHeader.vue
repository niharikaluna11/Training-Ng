<template>
    <header class="header">
        <div class="header-content responsive-wrapper">
            <div class="header-logo">
                <a href="#">
                    <div>
                        <img src="https://assets.codepen.io/285131/untitled-ui-icon.svg" />
                    </div>
                    <img src="https://assets.codepen.io/285131/untitled-ui.svg" />
                </a>
            </div>
            <div class="header-navigation">
                <nav class="header-navigation-links">
                    <router-link to="/HomePage" class="nav-link active text-light" aria-current="page">
                        Home
                    </router-link>
                </nav>
                <div class="header-navigation-actions">
                    <a href="#" class="button">
                        <i class="ph-lightning-bold"></i>
                        <span>Upgrade now</span>
                    </a>
                    <a href="#" class="icon-button">
                        <i class="ph-gear-bold"></i>
                    </a>
                    <a href="#" class="icon-button">
                        <i class="ph-bell-bold"></i>
                    </a>
                    <a href="#" class="avatar">
                        <img src="https://assets.codepen.io/285131/hat-man.png" alt="" />
                    </a>
                </div>
            </div>
        </div>
    </header>
</template>

<script>
export default {
    name: 'AdminHeader',
};
</script>

<style scoped>
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
