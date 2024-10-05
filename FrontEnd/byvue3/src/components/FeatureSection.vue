<script setup>

</script>

<template>
    <section class="feature feature--invert cd-position-relative cd-z-index-1 cd-margin-y-2xl">
        <div class="cd-container cd-max-width-adaptive-lg">
            <div class="feature__grid">
                <div>
                    <div class="feature__content">
                        <p class="feature__label">Pills &amp; potions</p>
                        <h1>I have a dream</h1>
                        <p class="cd-line-height-lg">Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa, rem
                            neque doloribus impedit explicabo minus laborum cupiditate atque esse consectetur.</p>

                        <div class="feature__actions">
                            <a href="#0" class="cd-btn cd-btn--primary">Download</a>
                            <a href="#0" class="cd-link">Learn more</a>
                        </div>
                    </div>
                </div>

                <div>
                    <figure>
                        <img class="feature__img" data-img-light
                            src="https://d3ae4wrz2ljkbq.cloudfront.net/components/img-3-2-light-1.svg"
                            alt="Image description">
                    </figure>
                </div>
            </div>
        </div>
    </section>
</template>

<style scoped>
/* -------------------------------- 

File#: _1_feature
Title: Feature
Descr: Content group containing text + media (img, video, iframe, inline SVG)
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

Buttons 

-------------------------------- */

.cd-btn {
    position: relative;
    display: inline-flex;
    justify-content: center;
    align-items: center;
    white-space: nowrap;
    text-decoration: none;
    padding: 0.5rem 1rem;
    border-radius: 0.375em;
    font-size: 1em;
    font-weight: 500;
    text-decoration: none;
    line-height: 1.2;
    cursor: pointer;
    transition: 0.2s;
    will-change: transform;
}

.cd-btn:focus-visible {
    outline: none;
}

.cd-btn:active {
    transform: translateY(2px);
}

.cd-btn--primary {
    background: hsl(250, 84%, 54%);
    box-shadow: inset 0 1px 0 hsla(0, 0%, 100%, 0.15),
        0 1px 3px hsla(250, 84%, 38%, 0.25),
        0 2px 6px hsla(250, 84%, 38%, 0.1),
        0 6px 10px -2px hsla(250, 84%, 38%, 0.25);
    color: hsl(0, 0%, 100%);
}

.cd-btn--primary:hover {
    background: hsl(250, 84%, 60%);
    box-shadow: inset 0 1px 0 hsla(0, 0%, 100%, 0.15),
        0 1px 2px hsla(250, 84%, 38%, 0.25),
        0 1px 4px hsla(250, 84%, 38%, 0.1),
        0 3px 6px -2px hsla(250, 84%, 38%, 0.25);
}

.cd-btn--primary:focus-visible {
    box-shadow: inset 0 1px 0 hsla(0, 0%, 100%, 0.15),
        0 1px 2px hsla(250, 84%, 38%, 0.25),
        0 1px 4px hsla(250, 84%, 38%, 0.1),
        0 3px 6px -2px hsla(250, 84%, 38%, 0.25),
        0 0 0 2px hsl(0, 0%, 100%),
        0 0 0 4px hsl(250, 84%, 54%);
}

/* --------------------------------

Component 

-------------------------------- */

.feature {}

.feature__grid {
    display: grid;
    gap: 2rem;
    align-items: center;
}

.feature__grid>* {
    min-width: 0;
}

.feature__content {
    display: flex;
    gap: 1rem;
    flex-direction: column;
}

.feature__label {
    font-size: 0.9375rem;
    color: hsl(240, 4%, 65%);
    text-transform: capitalize;
}

.feature__actions {
    display: flex;
    flex-wrap: wrap;
    align-items: center;
    gap: 1rem;
}

.feature--center-x .feature__actions {
    justify-content: center;
}

.feature__img {
    width: 100%;
    border-radius: 0.75em;
}

@media (min-width: 64rem) {
    .feature__grid {
        grid-template-columns: repeat(2, 1fr);
    }

    .feature--invert .feature__grid>*:first-child {
        order: 2;
    }
}

/* -------------------------------- 

Utilities 

-------------------------------- */

.cd-container {
    width: calc(100% - 3rem);
    margin-left: auto;
    margin-right: auto;
}

.cd-max-width-adaptive-lg {
    max-width: 32rem;
}

@media (min-width: 48rem) {
    .cd-max-width-adaptive-lg {
        max-width: 48rem;
    }
}

@media (min-width: 64rem) {
    .cd-max-width-adaptive-lg {
        max-width: 64rem;
    }
}

@media (min-width: 80rem) {
    .cd-max-width-adaptive-lg {
        max-width: 80rem;
    }
}

.cd-position-relative {
    position: relative;
}

.cd-z-index-1 {
    z-index: 1;
}

.cd-margin-y-2xl {
    margin-top: 4rem;
    margin-bottom: 4rem;
}

.cd-text-center {
    text-align: center;
}

.cd-line-height-lg {
    line-height: 1.58;
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
</style>