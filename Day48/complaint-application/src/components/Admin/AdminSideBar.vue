<script>
export default {
    name: 'AdminSideBar'
};
</script>

<template>
    <div class="css-q334zl">
        <div class="css-1sausy9">
            <div class="css-nqshte" role="group" data-section="true">
                <div role="heading" aria-level="2" data-ds--menu--heading-item="true"
                    class="sc-jKVCRD ernfUy css-nsg8gc"></div>
                <div role="presentation">
                    <a data-vc="link-item" class="css-1t4qrb2" href="/" draggable="false" aria-current="page"
                        aria-disabled="false">
                        <span class="css-12dc8u4">
                            <span data-item-elem-before="true" class="css-1rnc8rc">
                                <span data-vc="icon-undefined" aria-hidden="true" class="css-snhnyn"
                                    style="--icon-primary-color: currentColor; --icon-secondary-color: var(--ds-surface, #FFFFFF);">
                                    <svg width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path
                                            d="m12.44 3.102.115.066 9 6a1 1 0 0 1-1.009 1.723l-.1-.059-.446-.297V18a2 2 0 0 1-1.85 1.994L18 20h-3a2 2 0 0 1-1.995-1.85L13 18v-4h-2v4a2 2 0 0 1-1.85 1.994L9 20H6a2 2 0 0 1-1.995-1.85L4 18v-7.465l-.445.297a1 1 0 0 1-1.317-.184l-.07-.093a1 1 0 0 1 .184-1.317l.093-.07 9-6a1 1 0 0 1 .994-.066Zm-.44 2.1-6 4V18h3v-4a2 2 0 0 1 1.85-1.995L11 12h2a2 2 0 0 1 1.995 1.85L15 14v4h3V9.202l-6-4Z"
                                            fill="currentColor"></path>
                                    </svg>
                                </span>
                            </span>
                            <div class="css-c93nfn">
                                <span class="css-uokyes" data-item-title="true">Home</span>
                            </div>
                        </span>
                    </a>
                </div>
                <div role="presentation">
                    <a data-vc="link-item" class="css-1njpl1b" href="/updates" draggable="false" aria-disabled="false">
                        <span class="css-12dc8u4">
                            <span data-item-elem-before="true" class="css-1rnc8rc">
                                <span data-vc="icon-undefined" aria-hidden="true" class="css-snhnyn"
                                    style="--icon-primary-color: var(--ds-icon); --icon-secondary-color: var(--ds-surface, #FFFFFF);">
                                    <svg width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path fill-rule="evenodd" clip-rule="evenodd"
                                            d="M8 4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h3v1a1 1 0 1 0 2 0v-1h3a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2H8Zm4 14h4V6H8v12h4Zm0-7a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Zm1.5 3.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0Z"
                                            fill="currentColor"></path>
                                    </svg>
                                </span>
                            </span>
                            <div class="css-c93nfn">
                                <span class="css-uokyes" data-item-title="true">Status Updates</span>
                            </div>
                        </span>
                    </a>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
body {
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
}

.css-q334zl {
    background-color: var(--c-background-secondary);
    padding: 10px;
}

.css-1sausy9 {
    display: flex;
    flex-direction: column;
    gap: 10px;
}

.css-1t4qrb2,
.css-1njpl1b {
    display: flex;
    align-items: center;
    text-decoration: none;
    color: #333;
    padding: 10px;
    border-radius: 5px;
    transition: background-color 0.3s ease;
}

.css-1t4qrb2:hover,
.css-1njpl1b:hover {
    background-color: #e6e6e6;
}

.css-12dc8u4 {
    display: flex;
    align-items: center;
}

.css-1rnc8rc {
    margin-right: 10px;
}

.css-c93nfn {
    display: flex;
    align-items: center;
}

.css-uokyes {
    font-size: 16px;
    font-weight: bold;
}
</style>