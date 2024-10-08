<script setup>
import { computed, defineProps, onMounted } from 'vue';

const props = defineProps({
    // Normal property declarations
    totalPages: Number,
    currentPage: Number,
    // Styles property declarations
    styles: Object
});
onMounted(() => {
    for (const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
});
const pages = computed(() => {
    const pages = [];

    // total pages less than 4 (1, 2, 3, 4)
    if (props.totalPages <= 4) {
        for (let i = 1; i <= props.totalPages; i++) {
            pages.push(i);
        }
    }
    // total pages more than 4 (5, 6, 7, 8, 9, 10,........)
    else {
        if (props.currentPage === 1) {
            pages.push(1, 2, 3);
        }
        // current page is the centerpage (2, 3, 4,..., totalpage-1)
        // ex: current: 2 && total pages: 20 => 1, 2, 3 [,..., 20]
        // ex: current: 17 && total pages: 20 => [1,...,] 16, 17, 18 [,..., 20]
        else if (props.currentPage > 1 && props.currentPage < props.totalPages && props.totalPages - props.currentPage >= 2) {
            pages.push(props.currentPage - 1, props.currentPage, props.currentPage + 1);
        }
        // current page is the last of pages (total page-2, total page-1, total page)
        // ex: current: 18 && total pages: 20 => [1,...,] 18, 19, 20
        else if (props.currentPage >= props.totalPages - 2) {
            pages.push(props.totalPages - 2, props.totalPages - 1, props.totalPages);
        }
    }
    return pages;
})

</script>

<template>
    <nav class="pagination " aria-label="Pagination">
        <ol class="pagination__list pt4-flex pt4-flex-wrap pt4-gap-3xs pt4-justify-center">
            <li>
                <router-link
                :to="`?page=1`"
                :class="['pagination__item', {'pagination__item--disabled' : props.currentPage === 1}]" 
                aria-label="Go to previous page"
                >
                    <svg class="pt4-icon pt4-icon--xs pt4-margin-right-3xs pt4-flip-x" viewBox="0 0 16 16">
                        <polyline points="6 2 12 8 6 14" fill="none" stroke="currentColor" stroke-linecap="round"
                            stroke-linejoin="round" stroke-width="2" />
                    </svg>
                    <span>To The First</span>
                </router-link>
            </li>
            <li>
                <router-link
                :to="`?page=${props.currentPage - 1}`"
                :class="['pagination__item', {'pagination__item--disabled' : props.currentPage === 1}]" 
                aria-label="Go to previous page"
                >
                    <svg class="pt4-icon pt4-icon--xs pt4-margin-right-3xs pt4-flip-x" viewBox="0 0 16 16">
                        <polyline points="6 2 12 8 6 14" fill="none" stroke="currentColor" stroke-linecap="round"
                            stroke-linejoin="round" stroke-width="2" />
                    </svg>
                    <span>Prev</span>
                </router-link>
            </li>
            <li class="pt4-display@sm"
            aria-hidden="true" 
            v-if="props.totalPages >= 5 && props.currentPage >= 3"
            >
                <span class="pagination__item pagination__item--ellipsis">1</span>
            </li>
            <li class="pt4-display@sm"
            aria-hidden="true" 
            v-if="props.totalPages >= 5 && props.currentPage >= 4"
            >
                <span class="pagination__item pagination__item--ellipsis">...</span>
            </li>
            <li v-for="page in pages" :key="page" :v-if="props.totalPages <= 4">
                <router-link
                :to="`?page=${page}`" 
                :class="['pagination__item',{'pagination__item--selected' : page === props.currentPage}]"
                :aria-label="`Go to page ${ page }`"
                >
                    {{ page }}
                </router-link>
            </li>
            <li class="pt4-display@sm"
            aria-hidden="true" 
            v-if="props.totalPages >= 5 && props.totalPages - props.currentPage >= 3"
            >
                <span class="pagination__item pagination__item--ellipsis">...</span>
            </li>

            <li class="pt4-display@sm"
            v-if="props.totalPages - props.currentPage >= 2">
                <router-link 
                :to="`?page=${props.totalPages}`"
                class="pagination__item" 
                :aria-label="`Go to page ${props.totalPages}`">
                {{ props.totalPages }}
            </router-link>
            </li>

            <li>
                <router-link
                :to="`?page=${props.currentPage + 1}`"
                :class="['pagination__item', {'pagination__item--disabled' : props.currentPage === props.totalPages}]" 
                aria-label="Go to next page"
                >
                    <span>Next</span>
                    <svg class="pt4-icon pt4-icon--xs pt4-margin-left-3xs" viewBox="0 0 16 16">
                        <polyline points="6 2 12 8 6 14" fill="none" stroke="currentColor" stroke-linecap="round"
                            stroke-linejoin="round" stroke-width="2" />
                    </svg>
                </router-link>
            </li>
            <li>
                <router-link
                :to="`?page=${props.totalPages}`"
                :class="['pagination__item', {'pagination__item--disabled' : props.currentPage === props.totalPages}]" 
                aria-label="Go to next page"
                >
                    <span>To The Last</span>
                    <svg class="pt4-icon pt4-icon--xs pt4-margin-left-3xs" viewBox="0 0 16 16">
                        <polyline points="6 2 12 8 6 14" fill="none" stroke="currentColor" stroke-linecap="round"
                            stroke-linejoin="round" stroke-width="2" />
                    </svg>
                </router-link>
            </li>
        </ol>
    </nav>
</template>

<style scoped>
/* -------------------------------- 

File#: _1_pagination
Title: Pagination 
Descr: Component used to navigate through pages of related content
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

@media(min-width: 64rem) {
    :root {
        /* spacing */
        --pt4-space-3xs: 0.375rem;
        --pt4-space-xs: 0.75rem;
        --pt4-space-2xs: 0.5625rem;
    }
}

/* form elements */
.pt4-form-control {
    font-size: 1em;
    padding: 0.375rem 0.5rem;
    /* Thay thế bằng giá trị xác định */
    background: hsl(240, 4%, 95%);
    line-height: 1.2;
    box-shadow: inset 0px 0px 0px 1px hsl(240, 4%, 85%);
    transition: all 0.2s ease;
    border-radius: 0.25em;
}

.pt4-form-control::placeholder {
    opacity: 1;
    color: hsl(240, 4%, 65%);
}

.pt4-form-control:focus,
.pt4-form-control:focus-within {
    background: hsl(0, 0%, 100%);
    box-shadow: inset 0px 0px 0px 1px hsla(240, 4%, 85%, 0), 0px 0px 0px 2px hsl(250, 84%, 54%), 0 0.3px 0.4px rgba(0, 0, 0, 0.025), 0 0.9px 1.5px rgba(0, 0, 0, 0.05), 0 3.5px 6px rgba(0, 0, 0, 0.1);
    outline: none;
}

/* icons */
.pt4-icon {
    height: 1em;
    width: 1em;
    display: inline-block;
    color: inherit;
    fill: currentColor;
    line-height: 1;
    flex-shrink: 0;
    max-width: initial;
}

.pt4-icon--xs {
    height: 16px;
    /* Thay thế bằng giá trị xác định */
    width: 16px;
    /* Thay thế bằng giá trị xác định */
}

/* component */
.pagination__list>li {
    display: inline-block;
}

.pagination--split .pagination__list {
    width: 100%;
}

.pagination--split .pagination__list>*:first-child {
    margin-right: auto;
}

.pagination--split .pagination__list>*:last-child {
    margin-left: auto;
}

.pagination__item {
    display: inline-block;
    display: inline-flex;
    height: 100%;
    align-items: center;
    padding: 0.5rem calc(1.355 * 0.5rem);
    /* Thay thế bằng giá trị xác định */
    white-space: nowrap;
    line-height: 1;
    border-radius: 0.25em;
    text-decoration: none;
    color: hsl(230, 7%, 23%);
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    will-change: transform;
    transition: 0.2s;
}

.pagination__item:hover:not(.pagination__item--selected):not(.pagination__item--ellipsis) {
    background-color: hsla(230, 13%, 9%, 0.1);
}

.pagination__item:active {
    -webkit-transform: translateY(2px);
    transform: translateY(2px);
}

.pagination__item--selected {
    background-color: hsl(230, 13%, 9%);
    color: hsl(0, 0%, 100%);
    box-shadow: 0 0.3px 0.4px rgba(0, 0, 0, 0.025), 0 0.9px 1.5px rgba(0, 0, 0, 0.05), 0 3.5px 6px rgba(0, 0, 0, 0.1);
}

.pagination__item--disabled {
    opacity: 0.5;
    pointer-events: none;
}

.pagination__jumper input {
    width: 3em;
    margin-right: 0.5rem;
    /* Thay thế bằng giá trị xác định */
}

.pagination__jumper em {
    flex-shrink: 0;
    white-space: nowrap;
}

/* utility classes */
.pt4-items-center {
    align-items: center;
}

.pt4-flex {
    display: flex;
}

.pt4-flip-x {
    transform: scaleX(-1);
}

.pt4-justify-center {
    justify-content: center;
}

.pt4-gap-3xs {
    gap: 0.25rem;
    /* Thay thế bằng giá trị xác định */
}

.pt4-flex-wrap {
    flex-wrap: wrap;
}

.pt4-margin-left-3xs {
    margin-left: 0.25rem;
    /* Thay thế bằng giá trị xác định */
}

@media not all and (min-width: 48rem) {
    .pt4-display\@sm {
        display: none !important;
    }
}

.pt4-margin-right-3xs {
    margin-right: 0.25rem;
    /* Thay thế bằng giá trị xác định */
}
</style>