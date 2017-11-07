var gulp = require('gulp');
var util = require('gulp-util');
var sass = require('gulp-sass');
var nano = require('gulp-cssnano');
var prefix = require('gulp-autoprefixer');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');

gulp.task('test', function () {
    'use strict';

    console.log('\n\tGulp: I\'m fine. Thank you for your concern.\n\tGulp: :D\n');
});

gulp.task('css', function () {
    'use strict';

    return gulp.src('./_sass/**/*.scss')
        .pipe(sass({
            outputStyle: "expanded",
            indentWidth: 2,
            linefeed: "crlf",
            sourceComments: true
        }).on('error', sass.logError))
        .pipe(prefix({
            browsers: ['> 0.1%', 'IE > 8']
        }))
        .pipe(gulp.dest('./css/'))
        .pipe(nano())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('./css/'));
});

gulp.task('js', function () { 
    'use strict';

    return gulp.src(['./js/**/*.js', '!./js/**/*.min.js'])
        .pipe(uglify({
            mangle: false
        }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('./js/'));
});

gulp.task('build', ['css', 'js']);

gulp.task('start', ['build'], function () {
    'use strict';

    gulp.watch('./_sass/**/*.scss', ['css']);
    gulp.watch(['./js/**/*.js', '!./js/**/*.min.js'], ['js']);
});
