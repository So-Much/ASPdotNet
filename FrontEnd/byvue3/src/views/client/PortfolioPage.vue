<script setup>
import { injectMainJS, removeMainJS } from '@/utils/asynchronous';
import { onMounted, onUnmounted } from 'vue';

onMounted(() => {
    injectMainJS();
});
onUnmounted(() => {
    removeMainJS();
});
</script>

<template>
    <div class="is-preload">
        <!-- Wrapper -->
        <div id="wrapper">

            <!-- Header -->
            <header id="header">
                <div class="logo">
                    <span class="icon fa-gem"></span>
                </div>
                <div class="content">
                    <div class="inner">
                        <h1>Dimension</h1>
                        <p>A fully responsive site template designed by <a href="https://html5up.net">HTML5 UP</a> and
                            released<br />
                            for free under the <a href="https://html5up.net/license">Creative Commons</a> license.</p>
                    </div>
                </div>
                <nav>
                    <ul>
                        <li><a href="#intro">Intro</a></li>
                        <li><a href="#work">Work</a></li>
                        <li><a href="#about">About</a></li>
                        <li><a href="#contact">Contact</a></li>
                        <!--<li><a href="#elements">Elements</a></li>-->
                    </ul>
                </nav>
            </header>
            <!-- Footer -->
            <footer id="footer">
                <p class="copyright">&copy; Untitled. Design: <a href="https://html5up.net">HTML5 UP</a>.</p>
            </footer>

        </div>

        <!-- BG -->
        <div id="bg"></div>
    </div>
</template>

<style scoped></style>