/* eslint-disable no-undef */

var webpack = require('webpack');
var CleanWebpackPlugin = require('clean-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var autoprefixer = require('autoprefixer');
var postcssSimpleVars = require("postcss-simple-vars")
var postcssNested = require("postcss-nested")
var postcssImport = require("postcss-import")
var stylelint = require('stylelint')

exports.extractBundle = function(options) {
   const entry = [];
   entry[options.name] = options.entries;

   return {
      entry: entry,
      plugins: [
         // Extract bundle and manifest files. Manifest is
         // needed for reliable caching.
         new webpack.optimize.CommonsChunkPlugin({
            names: [options.name, 'manifest']
         })
      ]
   }
};

exports.minifyJS = function() {
   return {
      plugins: [
         new webpack.optimize.UglifyJsPlugin( {
            compress: {
               warnings: false
            },

            mangle: {
               except: ['webpackJsonp']
            }
         })
      ]
   }
};

exports.clean = function(path) {
   return {
      plugins: [
         new CleanWebpackPlugin([path], {
            // Without `root` CleanWebpackPlugin won't point to our
            // project and will fail to work.
            root: process.cwd(),
            "verbose": true
         })
      ]
   };
};

exports.extractCSS = function(paths) {
   return {
			module: {
				preLoaders: [
					{
						test: /(scss|css)$/,
						loaders: ['postcss'],
						include: paths
					},
				],
				loaders: [
					{
						test: /\.jsx?$/,
						exclude: /node_modules/,
						loader: "babel-loader"
					},
					{
						test: /(scss|css)$/,
						loaders: [
							'style',
							'css-loader?modules=true&sourceMap=true&localIdentName=[name]__[local]___[hash:base64:5]',
							'postcss'

						],
						include: paths
					},
					{
						test   : /\.(ttf|eot|svg|woff(2)?)(\?[a-z0-9=&.]+)?$/,
						loader : 'file-loader?name=fonts/[name].[hash].[ext]',
						include: paths
					}
				],
			},

			postcss: function(paths, webpack)  {

				return [
					autoprefixer({
						browsers: ['last 2 versions']
					}),

					postcssImport ({
						path: paths,
						addDependencyTo: webpack
					}),

					postcssSimpleVars,
					postcssNested
				]

			},
         
			plugins: [
				// Output extracted CSS to a file
				new ExtractTextPlugin('[name].css', {
					allChunks: true
				})
			]
	}
};

/* eslint-enable no-undef */