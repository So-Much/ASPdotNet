<script setup>

</script>

<template>
    <section class="feature-v6 cd-padding-y-xl">
        <div class="cd-container cd-max-width-adaptive-lg">
            <div class="feature-v6__grid">
                <div>
                    <img class="feature-v6__img"
                        src="https://d3ae4wrz2ljkbq.cloudfront.net/components/img-16-9-dark-5.svg"
                        alt="Image description">

                    <h4 class="cd-margin-bottom-2xs">Nothing is cheap</h4>

                    <p class="feature-v6__description cd-margin-bottom-xs">Lorem ipsum dolor sit amet consectetur
                        adipisicing elit. Praesentium assumenda doloribus eos provident modi.</p>

                    <p><a href="#0" class="cd-link">Learn more</a></p>
                </div>

                <div>
                    <img class="feature-v6__img"
                        src="https://d3ae4wrz2ljkbq.cloudfront.net/components/img-16-9-dark-5.svg"
                        alt="Image description">

                    <h4 class="cd-margin-bottom-2xs">The quick brown fox jumps over the lazy dog</h4>

                    <p class="feature-v6__description cd-margin-bottom-xs">Lorem ipsum dolor, sit amet consectetur
                        adipisicing elit. Quibusdam distinctio inventore architecto.</p>

                    <p><a href="#0" class="cd-link">Learn more</a></p>
                </div>

                <div>
                    <img class="feature-v6__img"
                        src="https://d3ae4wrz2ljkbq.cloudfront.net/components/img-16-9-dark-5.svg"
                        alt="Image description">

                    <h4 class="cd-margin-bottom-2xs">Welcome to the Web Academy</h4>

                    <p class="feature-v6__description cd-margin-bottom-xs">Lorem ipsum dolor sit amet consectetur
                        adipisicing elit. Praesentium assumenda doloribus eos provident modi.</p>

                    <p><a href="#0" class="cd-link">Learn more</a></p>
                </div>

                <div>
                    <img class="feature-v6__img"
                        src="https://d3ae4wrz2ljkbq.cloudfront.net/components/img-16-9-dark-5.svg"
                        alt="Image description">

                    <h4 class="cd-margin-bottom-2xs">Strength does not come from winning</h4>

                    <p class="feature-v6__description cd-margin-bottom-xs">Lorem ipsum dolor, sit amet consectetur
                        adipisicing elit. Quibusdam distinctio inventore architecto.</p>

                    <p><a href="#0" class="cd-link">Learn more</a></p>
                </div>
            </div>
        </div>
    </section>
</template>

<style scoped>
/* -------------------------------- 

File#: _1_feature-v6
Title: Feature v6
Descr: A section containing a list of features
Usage: codyhouse.co/license

-------------------------------- */

/* reset */
*,
*::after,
*::before {
    box-sizing: border-box;
}

* {
    font: inherit;
    margin: 0;
    padding: 0;
    border: 0;
}

html {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}

body {
    background-color: hsl(0, 0%, 100%);
    font-family: system-ui, sans-serif;
    color: hsl(230, 7%, 23%);
    font-size: 1.125rem;
    /* 18px */
    line-height: 1.4;
}

h1,
h2,
h3,
h4 {
    line-height: 1.2;
    color: hsl(230, 13%, 9%);
    font-weight: 700;
}

h1 {
    font-size: 2.5rem;
    /* 40px */
}

h2 {
    font-size: 2.125rem;
    /* 34px */
}

h3 {
    font-size: 1.75rem;
    /* 28px */
}

h4 {
    font-size: 1.375rem;
    /* 22px */
}

ol,
ul,
menu {
    list-style: none;
}

button,
input,
textarea,
select {
    background-color: transparent;
    border-radius: 0;
    color: inherit;
    line-height: inherit;
    -webkit-appearance: none;
    appearance: none;
}

textarea {
    resize: vertical;
    overflow: auto;
    vertical-align: top;
}

a {
    color: hsl(250, 84%, 54%);
}

table {
    border-collapse: collapse;
    border-spacing: 0;
}

img,
video,
svg {
    display: block;
    max-width: 100%;
}

/* -------------------------------- 

Icons 

-------------------------------- */

.cd-icon {
    --size: 1em;
    font-size: var(--size);
    height: 1em;
    width: 1em;
    display: inline-block;
    color: inherit;
    fill: currentColor;
    line-height: 1;
    flex-shrink: 0;
    max-width: initial;
}

.cd-icon use {
    /* SVG symbols - enable icon color corrections */
    color: inherit;
    fill: currentColor;
}

/* -------------------------------- 

Component 

-------------------------------- */

.feature-v6 {
    position: relative;
    z-index: 1;
}

.feature-v6__grid {
    display: grid;
    gap: 3rem;
}

.feature-v6__grid>* {
    min-width: 0;
}

.feature-v6__img {
    width: 100%;
    border-radius: 0.375em;
    margin-bottom: 1rem;
}

.feature-v6__icon-wrapper {
    flex-shrink: 0;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    box-shadow: 0 0 0 1px hsla(230, 13%, 9%, 0.05);
    background-color: hsl(0, 0%, 100%);
    margin-bottom: 1rem;
}

.feature-v6__icon {
    --size: 24px;
    color: hsl(250, 84%, 54%);
}

.feature-v6__description {
    color: hsl(225, 4%, 47%);
    line-height: 1.58;
}

@media (min-width: 64rem) {
    .feature-v6__grid {
        grid-template-columns: repeat(2, 1fr);
    }
}

/* -------------------------------- 

Utilities 

-------------------------------- */

.cd-margin-x-auto {
    margin-left: auto;
    margin-right: auto;
}

.cd-margin-bottom-2xs {
    margin-bottom: 0.75rem;
}

.cd-margin-bottom-xs {
    margin-bottom: 1rem;
}

.cd-padding-y-xl {
    padding-top: 4.5rem;
    padding-bottom: 4.5rem;
}

.cd-container {
    width: calc(100% - 3rem);
    margin-left: auto;
    margin-right: auto;
}

.cd-max-width-adaptive-lg {
    max-width: 32rem;
}

@media (min-width: 64rem) {
    .cd-max-width-adaptive-lg {
        max-width: 64rem;
    }
}

@media (min-width: 85.5rem) {
    .cd-max-width-adaptive-lg {
        max-width: 80rem;
    }
}

.cd-link {
    color: hsl(250, 84%, 54%);
    text-decoration: none;
    background-image: linear-gradient(to right, hsl(250, 84%, 54%) 50%, hsla(250, 84%, 54%, 0.2) 50%);
    background-size: 200% 1px;
    background-repeat: no-repeat;
    background-position: 100% 100%;
    transition: background-position 0.2s;
}

.cd-link:hover {
    background-position: 0% 100%;
}

.cd-text-center {
    text-align: center;
}
</style>