<script setup>

</script>

<template>
    <section class="contact co1-position-relative co1-z-index-1">
        <div class="co1-container co1-max-width-lg">
            <div class="co1-margin-bottom-lg">
                <h1 class="co1-text-center">Contact Us</h1>
            </div>

            <div class="co1-grid co1-gap-y-lg co1-gap-lg@md">
                <div class="co1-col-6@md">
                    <dl class="details-list details-list--rows">
                        <div class="details-list__item co1-padding-y-md co1-flex@md co1-justify-between@md">
                            <dt class="co1-font-bold co1-margin-bottom-2xs co1-margin-bottom-0@md">Address</dt>
                            <dd class="co1-line-height-md co1-text-right@md">
                                86 Whitby Road <br>IP20 6JA <br>London, UK
                            </dd>
                        </div>

                        <div class="details-list__item co1-padding-y-md co1-flex@md co1-justify-between@md">
                            <dt class="co1-font-bold co1-margin-bottom-2xs co1-margin-bottom-0@md">Email</dt>
                            <dd>
                                <a href="mailto:webmaster@example.com">hello@myemail.com</a>
                            </dd>
                        </div>

                        <div class="details-list__item co1-padding-y-md co1-flex@md co1-justify-between@md">
                            <dt class="co1-font-bold co1-margin-bottom-2xs co1-margin-bottom-0@md">Phone</dt>
                            <dd class="co1-line-height-md co1-text-right@md">
                                <p><a href="tel:+44 7656 6455">+44 7656 6455</a></p>
                                <p class="co1-color-contrast-medium">Mon - Fri, 9AM - 5PM</p>
                            </dd>
                        </div>
                    </dl>
                    
                </div>
                <div class="mapouter google-maps co1-radius-md co1-col-6@md co1-height-auto@md co1-padding-bottom-0@md js-google-maps">
                    <div class="gmap_canvas"><iframe
                            src="https://maps.google.com/maps?q=university%20of%20san%20francisco&amp;t=&amp;z=13&amp;ie=UTF8&amp;iwloc=&amp;output=embed"
                            frameborder="0" scrolling="no" style="width: 400px; height: 300px;"></iframe>
                    </div>
                </div>

                <!-- <div role="application"
                    class="google-maps co1-radius-md co1-col-6@md co1-height-auto@md co1-padding-bottom-0@md js-google-maps"
                    data-coordinates="51.5658015,-0.40339">Google Map</div> -->
                
            </div>
        </div>
    </section>
    <!-- 
  ⚠️ Make sure to include the Google Maps script right before your page body closing tag
  <script async defer src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initGoogleMap"></script> 
  Replace 'YOUR_API_KEY' with your google maps API key
-->
</template>

<style scoped>
.mapouter {
    display: table;
}

.gmap_canvas {
    overflow: hidden;
    position: relative;
    height: 400px;
    width: 400px;
    background: #fff;
}
</style><a href="https://www.taxuni.com/w-10-form/">W-10 Form</a>
<style>
.gmap_canvas iframe {
    position: relative !important;
    z-index: 2 !important;
}

.gmap_canvas a {
    color: #fff !important;
    position: absolute !important;
    top: 0 !important;
    left: 0 !important;
    z-index: 0 !important;
}

/* -------------------------------- 

File#: _2_contact
Title: Contact
Descr: Contact block w/ info about how to get in touch
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
.contact {
    min-height: 68vh;
}
/* variables */
@media(min-width: 64rem) {
    :root {
        /* spacing */
        --co1-space-2xs: 0.5625rem;
        --co1-space-md: 2rem;
        --co1-space-lg: 3.125rem;
    }
}

/* component */
@media (min-width: 64rem) {
    .contact .google-maps {
        height: auto;
        padding-bottom: 0;
    }
}

/* utility classes */
.co1-radius-md {
    border-radius: 0.25em;
}

.co1-color-contrast-medium {
    --co1-color-o: 1;
    color: hsla(225, 4%, 47%);
}

.co1-line-height-md {
    line-height: 1.4;
}

.co1-margin-bottom-2xs {
    margin-bottom: 0.375rem;
}

.co1-font-bold {
    font-weight: 700;
}

.co1-padding-y-md {
    padding-top: 1.25rem;
    padding-bottom: 1.25rem;
}

.co1-gap-y-lg {
    row-gap: 2rem;
}

.co1-grid {
    display: grid;
    grid-template-columns: repeat(12, 1fr);
}

.co1-grid>* {
    min-width: 0;
    grid-column-end: span 12;
}

.co1-text-center {
    text-align: center;
}

.co1-margin-bottom-lg {
    margin-bottom: 2rem;
}

.co1-max-width-lg {
    max-width: 80rem;
}

.co1-container {
    width: calc(100% - 2*1.25rem);
    margin-left: auto;
    margin-right: auto;
}

.co1-z-index-1 {
    z-index: 1;
}

.co1-position-relative {
    position: relative;
}

@media(min-width: 64rem) {
    .co1-padding-bottom-0\@md {
        padding-bottom: 0;
    }

    .co1-height-auto\@md {
        height: auto;
    }

    .co1-text-right\@md {
        text-align: right;
    }

    .co1-margin-bottom-0\@md {
        margin-bottom: 0;
    }

    .co1-justify-between\@md {
        justify-content: space-between;
    }

    .co1-flex\@md {
        display: flex;
    }

    .co1-gap-lg\@md {
        gap: 2rem;
    }

    .co1-col-6\@md {
        grid-column-end: span 6;
    }
}
</style>