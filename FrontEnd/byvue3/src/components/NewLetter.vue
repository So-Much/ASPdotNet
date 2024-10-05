<script setup>

</script>

<template>
    <section class="newsletter nx6-padding-y-xl nx6-text-center">
        <div class="nx6-container nx6-max-width-xs">
            <div class="nx6-text-component nx6-margin-bottom-md">
                <h2>Join our Newsletter</h2>
                <p>Get our monthly recap with the latest news.</p>
            </div>

            <form class="nx6-flex nx6-flex-wrap nx6-gap-2xs">
                <div class="nx6-flex-basis-100% nx6-flex-basis-auto@xs nx6-flex-grow-auto@xs nx6-min-width-0">
                    <input class="nx6-form-control nx6-width-100% nx6-height-100%" aria-label="Email" type="email"
                        placeholder="Email address">
                </div>

                <div class="nx6-flex-basis-100% nx6-flex-basis-auto@xs nx6-flex-grow-0@xs">
                    <button class="nx6-btn nx6-btn--primary nx6-width-100% nx6-height-100%">Subscribe</button>
                </div>
            </form>

            <p role="alert"
                class="nx6-bg-success nx6-bg-opacity-20% nx6-text-sm nx6-padding-xs nx6-radius-md nx6-margin-top-xs">
                <strong>Success!</strong> Please check your inbox for a confirmation email.</p>

            <div class="nx6-margin-top-xs">
                <small class="nx6-text-xs nx6-color-contrast-medium">By subscribing you agree to our <a
                        class="nx6-color-contrast-high" href="#0">privacy policy</a>.</small>
            </div>
        </div>
    </section>
</template>

<style scoped>
/* -------------------------------- 

File#: _1_newsletter
Title: Newsletter 
Descr: Newsletter form template
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

body {
    background-color: hsl(0, 0%, 100%);
    font-family: system-ui, sans-serif;
    color: hsl(230, 7%, 23%);
    font-size: 1rem;
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
    font-size: 2.0736rem;
}

h2 {
    font-size: 1.728rem;
}

h3 {
    font-size: 1.25rem;
}

h4 {
    font-size: 1.2rem;
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

@media (min-width: 64rem) {
    body {
        font-size: 1.25rem;
    }

    h1 {
        font-size: 3.051rem;
    }

    h2 {
        font-size: 2.44rem;
    }

    h3 {
        font-size: 1.75rem;
    }

    h4 {
        font-size: 1.5625rem;
    }
}

/* variables */

@media(min-width: 64rem) {
    :root {
        /* spacing */
        --nx6-space-2xs: 0.5625rem;
        --nx6-space-xs: 0.75rem;
        --nx6-space-md: 2rem;
        --nx6-space-xl: 5.125rem;
        --nx6-space-sm: 1.125rem;

        /* typography */
        --nx6-text-sm: 1rem;
        --nx6-text-xs: 0.8rem;
        --nx6-text-sm: 1rem;
    }
}

/* buttons */
.nx6-btn {
    position: relative;
    display: inline-flex;
    justify-content: center;
    align-items: center;
    font-size: 1em;
    white-space: nowrap;
    text-decoration: none;
    background: hsl(240, 4%, 95%);
    color: hsl(230, 13%, 9%);
    cursor: pointer;
    text-decoration: none;
    line-height: 1.2;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    transition: all 0.2s ease;
    will-change: transform;
    padding: 0.375rem 0.75rem;
    border-radius: 0.25em;
}

.nx6-btn:focus-visible {
    box-shadow: 0px 0px 0px 2px hsl(0, 0%, 100%), 0px 0px 0px 4px hsla(230, 13%, 9%, 0.15);
    outline: none;
}

.nx6-btn:active {
    transform: translateY(2px);
}

.nx6-btn--primary {
    background: hsl(250, 84%, 54%);
    color: hsl(0, 0%, 100%);
    box-shadow: inset 0px 1px 0px hsla(0, 0%, 100%, 0.15), 0px 1px 3px hsla(250, 84%, 38%, 0.25), 0px 2px 6px hsla(250, 84%, 38%, 0.1), 0px 6px 10px -2px hsla(250, 84%, 38%, 0.25);
}

.nx6-btn--primary:hover {
    background: hsl(250, 84%, 60%);
    box-shadow: inset 0px 1px 0px hsla(0, 0%, 100%, 0.15), 0px 1px 2px hsla(250, 84%, 38%, 0.25), 0px 1px 4px hsla(250, 84%, 38%, 0.1), 0px 3px 6px -2px hsla(250, 84%, 38%, 0.25);
}

.nx6-btn--primary:focus {
    box-shadow: inset 0px 1px 0px hsla(0, 0%, 100%, 0.15), 0px 1px 2px hsla(250, 84%, 38%, 0.25), 0px 1px 4px hsla(250, 84%, 38%, 0.1), 0px 3px 6px -2px hsla(250, 84%, 38%, 0.25), 0px 0px 0px 2px hsl(0, 0%, 100%), 0px 0px 0px 4px hsl(250, 84%, 54%);
}

/* form elements */
.nx6-form-control {
    font-size: 1em;
    padding: 0.375rem 0.5rem;
    background: hsl(240, 4%, 95%);
    line-height: 1.2;
    box-shadow: inset 0px 0px 0px 1px hsl(240, 4%, 85%);
    transition: all 0.2s ease;
    border-radius: 0.25em;
}

.nx6-form-control::placeholder {
    opacity: 1;
    color: hsl(240, 4%, 65%);
}

.nx6-form-control:focus,
.nx6-form-control:focus-within {
    background: hsl(0, 0%, 100%);
    box-shadow: inset 0px 0px 0px 1px hsla(240, 4%, 85%, 0), 0px 0px 0px 2px hsl(250, 84%, 54%), 0 0.3px 0.4px rgba(0, 0, 0, 0.025), 0 0.9px 1.5px rgba(0, 0, 0, 0.05), 0 3.5px 6px rgba(0, 0, 0, 0.1);
    outline: none;
}

/* component */
.newsletter {
    position: relative;
    z-index: 1;
}

/* utility classes */
.nx6-color-contrast-high {
    --nx6-color-o: 1;
    color: hsla(230, 7%, 23%, var(--nx6-color-o, 1));
}

.nx6-color-contrast-medium {
    --nx6-color-o: 1;
    color: hsla(225, 4%, 47%, var(--nx6-color-o, 1));
}

.nx6-text-xs {
    font-size: 0.694rem;
}

.nx6-margin-top-xs {
    margin-top: 0.5rem;
}

.nx6-radius-md {
    border-radius: 0.25em;
}

.nx6-padding-xs {
    padding: 0.5rem;
}

.nx6-text-sm {
    font-size: 0.833rem;
}

.nx6-bg-success {
    --nx6-bg-o: 1;
    background-color: hsla(170, 78%, 36%, var(--nx6-bg-o, 1));
}

.nx6-height-100\% {
    height: 100%;
}

.nx6-width-100\% {
    width: 100%;
}

.nx6-gap-2xs {
    gap: 0.375rem;
}

.nx6-grid {
    display: grid;
    grid-template-columns: repeat(12, 1fr);
}

.nx6-grid>* {
    min-width: 0;
    grid-column-end: span 12;
}

.nx6-margin-bottom-md {
    margin-bottom: 1.25rem;
}

.nx6-text-component :where(h1, h2, h3, h4) {
    line-height: var(--nx6-heading-line-height, 1.2);
    margin-top: calc(1.25rem * var(--nx6-space-multiplier, 1));
    margin-bottom: calc(0.75rem * var(--nx6-space-multiplier, 1));
}

.nx6-text-component :where(p, blockquote, ul li, ol li) {
    line-height: var(--nx6-body-line-height, 1.4);
}

.nx6-text-component :where(ul, ol, p, blockquote, .nx6-text-component__block) {
    margin-bottom: calc(0.75rem * var(--nx6-space-multiplier, 1));
}

.nx6-text-component :where(ul, ol) {
    padding-left: 1.25em;
}

.nx6-text-component ul :where(ul, ol),
.nx6-text-component ol :where(ul, ol) {
    padding-left: 1em;
    margin-bottom: 0;
}

.nx6-text-component ul {
    list-style-type: disc;
}

.nx6-text-component ol {
    list-style-type: decimal;
}

.nx6-text-component img {
    display: block;
    margin: 0 auto;
}

.nx6-text-component figcaption {
    margin-top: calc(0.5rem * var(--nx6-space-multiplier, 1));
    font-size: 0.833rem;
    text-align: center;
}

.nx6-text-component em {
    font-style: italic;
}

.nx6-text-component strong {
    font-weight: bold;
}

.nx6-text-component s {
    text-decoration: line-through;
}

.nx6-text-component u {
    text-decoration: underline;
}

.nx6-text-component mark {
    background-color: hsla(342, 89%, 48%, 0.2);
    color: inherit;
}

.nx6-text-component blockquote {
    padding-left: 1em;
    border-left: 4px solid hsl(240, 4%, 85%);
    font-style: italic;
}

.nx6-text-component hr {
    margin: calc(1.25rem * var(--nx6-space-multiplier, 1)) auto;
    background: hsl(240, 4%, 85%);
    height: 1px;
}

.nx6-text-component>*:first-child {
    margin-top: 0;
}

.nx6-text-component>*:last-child {
    margin-bottom: 0;
}

.nx6-text-component.nx6-line-height-xs {
    --nx6-heading-line-height: 1;
    --nx6-body-line-height: 1.1;
}

.nx6-text-component.nx6-line-height-sm {
    --nx6-heading-line-height: 1.1;
    --nx6-body-line-height: 1.2;
}

.nx6-text-component.nx6-line-height-md {
    --nx6-heading-line-height: 1.15;
    --nx6-body-line-height: 1.4;
}

.nx6-text-component.nx6-line-height-lg {
    --nx6-heading-line-height: 1.22;
    --nx6-body-line-height: 1.58;
}

.nx6-text-component.nx6-line-height-xl {
    --nx6-heading-line-height: 1.3;
    --nx6-body-line-height: 1.72;
}

.nx6-max-width-xs {
    max-width: 38rem;
}

.nx6-container {
    width: calc(100% - 2*1.25rem);
    margin-left: auto;
    margin-right: auto;
}

.nx6-text-center {
    text-align: center;
}

.nx6-padding-y-xl {
    padding-top: 3.25rem;
    padding-bottom: 3.25rem;
}

.nx6-flex-basis-100\% {
    flex-basis: 100%;
}

.nx6-min-width-0 {
    min-width: 0;
}

.nx6-flex-wrap {
    flex-wrap: wrap;
}

.nx6-flex {
    display: flex;
}

.nx6-max-width-adaptive-sm {
    max-width: 32rem;
}

@media(min-width: 48rem) {
    .nx6-max-width-adaptive-sm {
        max-width: 48rem;
    }
}

.nx6-col-12 {
    grid-column-end: span 12;
}

.nx6-bg-opacity-20\% {
    --nx6-bg-o: 0.2;
}

@media(min-width: 32rem) {
    .nx6-flex-grow-auto\@xs {
        flex-grow: 1;
        flex-basis: 0;
        min-width: 0;
    }

    .nx6-flex-basis-auto\@xs {
        flex-basis: auto;
    }

    .nx6-flex-grow-0\@xs {
        flex-grow: 0;
    }

    .nx6-col-6\@xs {
        grid-column-end: span 6;
    }
}

@media(min-width: 48rem) {
    .nx6-flex-grow-0\@sm {
        flex-grow: 0;
    }

    .nx6-flex-basis-auto\@sm {
        flex-basis: auto;
    }
}
</style>