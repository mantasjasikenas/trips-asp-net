// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//pull in jQuery and plugins
import $ from 'jquery'

import 'jquery-validation'
import 'jquery-validation-unobtrusive'
import 'jquery-datetimepicker'
import 'datatables.net'

//pull in bootstrap scripts
import 'bootstrap'

//make styles known to webpack
import '../css/index.scss'

//expose jQuery outside the module built by webpack
window.__jQuery = $;