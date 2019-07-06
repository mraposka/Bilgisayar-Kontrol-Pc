"function"!=typeof Object.create&&(Object.create=function(e){function l(){}return l.prototype=e,new l}),function(e,l,t,n){var i={init:function(l,t){var n=this;n.$elem=e(t),n.options=e.extend({},e.fn.owlCarousel.options,n.$elem.data(),l),n.userOptions=l,n.loadContent()},loadContent:function(){function l(e){if("function"==typeof t.options.jsonSuccess)t.options.jsonSuccess.apply(this,[e]);else{var l="";for(var n in e.owl)l+=e.owl[n].item;t.$elem.html(l)}t.logIn()}var t=this;if("function"==typeof t.options.beforeInit&&t.options.beforeInit.apply(this,[t.$elem]),"string"==typeof t.options.jsonPath){var n=t.options.jsonPath;e.getJSON(n,l)}else t.logIn()},logIn:function(){var e=this;e.$elem.data("owl-originalStyles",e.$elem.attr("style")).data("owl-originalClasses",e.$elem.attr("class")),e.$elem.css({opacity:0}),e.orignalItems=e.options.items,e.checkBrowser(),e.wrapperWidth=0,e.checkVisible,e.setVars()},setVars:function(){var e=this;return 0===e.$elem.children().length?!1:(e.baseClass(),e.eventTypes(),e.$userItems=e.$elem.children(),e.itemsAmount=e.$userItems.length,e.wrapItems(),e.$owlItems=e.$elem.find(".owl-item"),e.$owlWrapper=e.$elem.find(".owl-wrapper"),e.playDirection="next",e.prevItem=0,e.prevArr=[0],e.currentItem=0,e.customEvents(),e.onStartup(),void 0)},onStartup:function(){var e=this;e.updateItems(),e.calculateAll(),e.buildControls(),e.updateControls(),e.response(),e.moveEvents(),e.stopOnHover(),e.owlStatus(),e.options.transitionStyle!==!1&&e.transitionTypes(e.options.transitionStyle),e.options.autoPlay===!0&&(e.options.autoPlay=5e3),e.play(),e.$elem.find(".owl-wrapper").css("display","block"),e.$elem.is(":visible")?e.$elem.css("opacity",1):e.watchVisibility(),e.onstartup=!1,e.eachMoveUpdate(),"function"==typeof e.options.afterInit&&e.options.afterInit.apply(this,[e.$elem])},eachMoveUpdate:function(){var e=this;e.options.lazyLoad===!0&&e.lazyLoad(),e.options.autoHeight===!0&&e.autoHeight(),e.onVisibleItems(),"function"==typeof e.options.afterAction&&e.options.afterAction.apply(this,[e.$elem])},updateVars:function(){var e=this;"function"==typeof e.options.beforeUpdate&&e.options.beforeUpdate.apply(this,[e.$elem]),e.watchVisibility(),e.updateItems(),e.calculateAll(),e.updatePosition(),e.updateControls(),e.eachMoveUpdate(),"function"==typeof e.options.afterUpdate&&e.options.afterUpdate.apply(this,[e.$elem])},reload:function(){var e=this;setTimeout(function(){e.updateVars()},0)},watchVisibility:function(){var e=this;return e.$elem.is(":visible")!==!1?!1:(e.$elem.css({opacity:0}),clearInterval(e.autoPlayInterval),clearInterval(e.checkVisible),e.checkVisible=setInterval(function(){e.$elem.is(":visible")&&(e.reload(),e.$elem.animate({opacity:1},200),clearInterval(e.checkVisible))},500),void 0)},wrapItems:function(){var e=this;e.$userItems.wrapAll('<div class="owl-wrapper">').wrap('<div class="owl-item"></div>'),e.$elem.find(".owl-wrapper").wrap('<div class="owl-wrapper-outer">'),e.wrapperOuter=e.$elem.find(".owl-wrapper-outer"),e.$elem.css("display","block")},baseClass:function(){var e=this,l=e.$elem.hasClass(e.options.baseClass),t=e.$elem.hasClass(e.options.theme);l||e.$elem.addClass(e.options.baseClass),t||e.$elem.addClass(e.options.theme)},updateItems:function(){var l=this;if(l.options.responsive===!1)return!1;if(l.options.singleItem===!0)return l.options.items=l.orignalItems=1,l.options.itemsCustom=!1,l.options.itemsDesktop=!1,l.options.itemsDesktopSmall=!1,l.options.itemsTablet=!1,l.options.itemsTabletSmall=!1,l.options.itemsMobile=!1,!1;var t=e(l.options.responsiveBaseWidth).width();if(t>(l.options.itemsDesktop[0]||l.orignalItems)&&(l.options.items=l.orignalItems),"undefined"!=typeof l.options.itemsCustom&&l.options.itemsCustom!==!1){l.options.itemsCustom.sort(function(e,l){return e[0]-l[0]});for(var n in l.options.itemsCustom)"undefined"!=typeof l.options.itemsCustom[n]&&l.options.itemsCustom[n][0]<=t&&(l.options.items=l.options.itemsCustom[n][1])}else t<=l.options.itemsDesktop[0]&&l.options.itemsDesktop!==!1&&(l.options.items=l.options.itemsDesktop[1]),t<=l.options.itemsDesktopSmall[0]&&l.options.itemsDesktopSmall!==!1&&(l.options.items=l.options.itemsDesktopSmall[1]),t<=l.options.itemsTablet[0]&&l.options.itemsTablet!==!1&&(l.options.items=l.options.itemsTablet[1]),t<=l.options.itemsTabletSmall[0]&&l.options.itemsTabletSmall!==!1&&(l.options.items=l.options.itemsTabletSmall[1]),t<=l.options.itemsMobile[0]&&l.options.itemsMobile!==!1&&(l.options.items=l.options.itemsMobile[1]);l.options.items>l.itemsAmount&&l.options.itemsScaleUp===!0&&(l.options.items=l.itemsAmount)},response:function(){var t,n=this;if(n.options.responsive!==!0)return!1;var i=e(l).width();n.resizer=function(){e(l).width()!==i&&(n.options.autoPlay!==!1&&clearInterval(n.autoPlayInterval),clearTimeout(t),t=setTimeout(function(){i=e(l).width(),n.updateVars()},n.options.responsiveRefreshRate))},e(l).resize(n.resizer)},updatePosition:function(){var e=this;e.jumpTo(e.currentItem),e.options.autoPlay!==!1&&e.checkAp()},appendItemsSizes:function(){var l=this,t=0,n=l.itemsAmount-l.options.items;l.$owlItems.each(function(i){var a=e(this);a.css({width:l.itemWidth}).data("owl-item",Number(i)),(0===i%l.options.items||i===n)&&(i>n||(t+=1)),a.data("owl-roundPages",t)})},appendWrapperSizes:function(){var e=this,l=0,l=e.$owlItems.length*e.itemWidth;e.$owlWrapper.css({width:2*l,left:0}),e.appendItemsSizes()},calculateAll:function(){var e=this;e.calculateWidth(),e.appendWrapperSizes(),e.loops(),e.max()},calculateWidth:function(){var e=this;e.itemWidth=Math.round(e.$elem.width()/e.options.items)},max:function(){var e=this,l=-1*(e.itemsAmount*e.itemWidth-e.options.items*e.itemWidth);return e.options.items>e.itemsAmount?(e.maximumItem=0,l=0,e.maximumPixels=0):(e.maximumItem=e.itemsAmount-e.options.items,e.maximumPixels=l),l},min:function(){return 0},loops:function(){var l=this;l.positionsInArray=[0],l.pagesInArray=[];for(var t=0,n=0,i=0;i<l.itemsAmount;i++)if(n+=l.itemWidth,l.positionsInArray.push(-n),l.options.scrollPerPage===!0){var a=e(l.$owlItems[i]),o=a.data("owl-roundPages");o!==t&&(l.pagesInArray[t]=l.positionsInArray[i],t=o)}},buildControls:function(){var l=this;(l.options.navigation===!0||l.options.pagination===!0)&&(l.owlControls=e('<div class="owl-controls"/>').toggleClass("clickable",!l.browser.isTouch).appendTo(l.$elem)),l.options.pagination===!0&&l.buildPagination(),l.options.navigation===!0&&l.buildButtons()},buildButtons:function(){var l=this,t=e('<div class="owl-buttons"/>');l.owlControls.append(t),l.buttonPrev=e("<div/>",{"class":"owl-prev",html:l.options.navigationText[0]||""}),l.buttonNext=e("<div/>",{"class":"owl-next",html:l.options.navigationText[1]||""}),t.append(l.buttonPrev).append(l.buttonNext),t.on("touchstart.owlControls mousedown.owlControls",'div[class^="owl"]',function(e){e.preventDefault()}),t.on("touchend.owlControls mouseup.owlControls",'div[class^="owl"]',function(t){t.preventDefault(),e(this).hasClass("owl-next")?l.next():l.prev()})},buildPagination:function(){var l=this;l.paginationWrapper=e('<div class="owl-pagination"/>'),l.owlControls.append(l.paginationWrapper),l.paginationWrapper.on("touchend.owlControls mouseup.owlControls",".owl-page",function(t){t.preventDefault(),Number(e(this).data("owl-page"))!==l.currentItem&&l.goTo(Number(e(this).data("owl-page")),!0)})},updatePagination:function(){var l=this;if(l.options.pagination===!1)return!1;l.paginationWrapper.html("");for(var t=0,n=l.itemsAmount-l.itemsAmount%l.options.items,i=0;i<l.itemsAmount;i++)if(0===i%l.options.items){if(t+=1,n===i)var a=l.itemsAmount-l.options.items;var o=e("<div/>",{"class":"owl-page"}),r=e("<span></span>",{text:l.options.paginationNumbers===!0?t:"","class":l.options.paginationNumbers===!0?"owl-numbers":""});o.append(r),o.data("owl-page",n===i?a:i),o.data("owl-roundPages",t),l.paginationWrapper.append(o)}l.checkPagination()},checkPagination:function(){var l=this;return l.options.pagination===!1?!1:(l.paginationWrapper.find(".owl-page").each(function(){e(this).data("owl-roundPages")===e(l.$owlItems[l.currentItem]).data("owl-roundPages")&&(l.paginationWrapper.find(".owl-page").removeClass("active"),e(this).addClass("active"))}),void 0)},checkNavigation:function(){var e=this;return e.options.navigation===!1?!1:(e.options.rewindNav===!1&&(0===e.currentItem&&0===e.maximumItem?(e.buttonPrev.addClass("disabled"),e.buttonNext.addClass("disabled")):0===e.currentItem&&0!==e.maximumItem?(e.buttonPrev.addClass("disabled"),e.buttonNext.removeClass("disabled")):e.currentItem===e.maximumItem?(e.buttonPrev.removeClass("disabled"),e.buttonNext.addClass("disabled")):0!==e.currentItem&&e.currentItem!==e.maximumItem&&(e.buttonPrev.removeClass("disabled"),e.buttonNext.removeClass("disabled"))),void 0)},updateControls:function(){var e=this;e.updatePagination(),e.checkNavigation(),e.owlControls&&(e.options.items>=e.itemsAmount?e.owlControls.hide():e.owlControls.show())},destroyControls:function(){var e=this;e.owlControls&&e.owlControls.remove()},next:function(e){var l=this;if(l.isTransition)return!1;if(l.currentItem+=l.options.scrollPerPage===!0?l.options.items:1,l.currentItem>l.maximumItem+(1==l.options.scrollPerPage?l.options.items-1:0)){if(l.options.rewindNav!==!0)return l.currentItem=l.maximumItem,!1;l.currentItem=0,e="rewind"}l.goTo(l.currentItem,e)},prev:function(e){var l=this;if(l.isTransition)return!1;if(l.options.scrollPerPage===!0&&l.currentItem>0&&l.currentItem<l.options.items?l.currentItem=0:l.currentItem-=l.options.scrollPerPage===!0?l.options.items:1,l.currentItem<0){if(l.options.rewindNav!==!0)return l.currentItem=0,!1;l.currentItem=l.maximumItem,e="rewind"}l.goTo(l.currentItem,e)},goTo:function(e,l,t){var n=this;if(n.isTransition)return!1;if("function"==typeof n.options.beforeMove&&n.options.beforeMove.apply(this,[n.$elem]),e>=n.maximumItem?e=n.maximumItem:0>=e&&(e=0),n.currentItem=n.owl.currentItem=e,n.options.transitionStyle!==!1&&"drag"!==t&&1===n.options.items&&n.browser.support3d===!0)return n.swapSpeed(0),n.browser.support3d===!0?n.transition3d(n.positionsInArray[e]):n.css2slide(n.positionsInArray[e],1),n.afterGo(),n.singleItemTransition(),!1;var i=n.positionsInArray[e];n.browser.support3d===!0?(n.isCss3Finish=!1,l===!0?(n.swapSpeed("paginationSpeed"),setTimeout(function(){n.isCss3Finish=!0},n.options.paginationSpeed)):"rewind"===l?(n.swapSpeed(n.options.rewindSpeed),setTimeout(function(){n.isCss3Finish=!0},n.options.rewindSpeed)):(n.swapSpeed("slideSpeed"),setTimeout(function(){n.isCss3Finish=!0},n.options.slideSpeed)),n.transition3d(i)):l===!0?n.css2slide(i,n.options.paginationSpeed):"rewind"===l?n.css2slide(i,n.options.rewindSpeed):n.css2slide(i,n.options.slideSpeed),n.afterGo()},jumpTo:function(e){var l=this;"function"==typeof l.options.beforeMove&&l.options.beforeMove.apply(this,[l.$elem]),e>=l.maximumItem||-1===e?e=l.maximumItem:0>=e&&(e=0),l.swapSpeed(0),l.browser.support3d===!0?l.transition3d(l.positionsInArray[e]):l.css2slide(l.positionsInArray[e],1),l.currentItem=l.owl.currentItem=e,l.afterGo()},afterGo:function(){var e=this;e.prevArr.push(e.currentItem),e.prevItem=e.owl.prevItem=e.prevArr[e.prevArr.length-2],e.prevArr.shift(0),e.prevItem!==e.currentItem&&(e.checkPagination(),e.checkNavigation(),e.eachMoveUpdate(),e.options.autoPlay!==!1&&e.checkAp()),"function"==typeof e.options.afterMove&&e.prevItem!==e.currentItem&&e.options.afterMove.apply(this,[e.$elem])},stop:function(){var e=this;e.apStatus="stop",clearInterval(e.autoPlayInterval)},checkAp:function(){var e=this;"stop"!==e.apStatus&&e.play()},play:function(){var e=this;return e.apStatus="play",e.options.autoPlay===!1?!1:(clearInterval(e.autoPlayInterval),e.autoPlayInterval=setInterval(function(){e.next(!0)},e.options.autoPlay),void 0)},swapSpeed:function(e){var l=this;"slideSpeed"===e?l.$owlWrapper.css(l.addCssSpeed(l.options.slideSpeed)):"paginationSpeed"===e?l.$owlWrapper.css(l.addCssSpeed(l.options.paginationSpeed)):"string"!=typeof e&&l.$owlWrapper.css(l.addCssSpeed(e))},addCssSpeed:function(e){return{"-webkit-transition":"all "+e+"ms ease","-moz-transition":"all "+e+"ms ease","-o-transition":"all "+e+"ms ease",transition:"all "+e+"ms ease"}},removeTransition:function(){return{"-webkit-transition":"","-moz-transition":"","-o-transition":"",transition:""}},doTranslate:function(e){return{"-webkit-transform":"translate3d("+e+"px, 0px, 0px)","-moz-transform":"translate3d("+e+"px, 0px, 0px)","-o-transform":"translate3d("+e+"px, 0px, 0px)","-ms-transform":"translate3d("+e+"px, 0px, 0px)",transform:"translate3d("+e+"px, 0px,0px)"}},transition3d:function(e){var l=this;l.$owlWrapper.css(l.doTranslate(e))},css2move:function(e){var l=this;l.$owlWrapper.css({left:e})},css2slide:function(e,l){var t=this;t.isCssFinish=!1,t.$owlWrapper.stop(!0,!0).animate({left:e},{duration:l||t.options.slideSpeed,complete:function(){t.isCssFinish=!0}})},checkBrowser:function(){var e=this,n="translate3d(0px, 0px, 0px)",i=t.createElement("div");i.style.cssText="  -moz-transform:"+n+"; -ms-transform:"+n+"; -o-transform:"+n+"; -webkit-transform:"+n+"; transform:"+n;var a=/translate3d\(0px, 0px, 0px\)/g,o=i.style.cssText.match(a),r=null!==o&&1===o.length,s="ontouchstart"in l||navigator.msMaxTouchPoints;e.browser={support3d:r,isTouch:s}},moveEvents:function(){var e=this;(e.options.mouseDrag!==!1||e.options.touchDrag!==!1)&&(e.gestures(),e.disabledEvents())},eventTypes:function(){var e=this,l=["s","e","x"];e.ev_types={},e.options.mouseDrag===!0&&e.options.touchDrag===!0?l=["touchstart.owl mousedown.owl","touchmove.owl mousemove.owl","touchend.owl touchcancel.owl mouseup.owl"]:e.options.mouseDrag===!1&&e.options.touchDrag===!0?l=["touchstart.owl","touchmove.owl","touchend.owl touchcancel.owl"]:e.options.mouseDrag===!0&&e.options.touchDrag===!1&&(l=["mousedown.owl","mousemove.owl","mouseup.owl"]),e.ev_types.start=l[0],e.ev_types.move=l[1],e.ev_types.end=l[2]},disabledEvents:function(){var l=this;l.$elem.on("dragstart.owl",function(e){e.preventDefault()}),l.$elem.on("mousedown.disableTextSelect",function(l){return e(l.target).is("input, textarea, select, option")})},gestures:function(){function i(e){return e.touches?{x:e.touches[0].pageX,y:e.touches[0].pageY}:e.pageX!==n?{x:e.pageX,y:e.pageY}:{x:e.clientX,y:e.clientY}}function a(l){"on"===l?(e(t).on(c.ev_types.move,r),e(t).on(c.ev_types.end,s)):"off"===l&&(e(t).off(c.ev_types.move),e(t).off(c.ev_types.end))}function o(t){var t=t.originalEvent||t||l.event;if(3===t.which)return!1;if(!(c.itemsAmount<=c.options.items)){if(c.isCssFinish===!1&&!c.options.dragBeforeAnimFinish)return!1;if(c.isCss3Finish===!1&&!c.options.dragBeforeAnimFinish)return!1;c.options.autoPlay!==!1&&clearInterval(c.autoPlayInterval),c.browser.isTouch===!0||c.$owlWrapper.hasClass("grabbing")||c.$owlWrapper.addClass("grabbing"),c.newPosX=0,c.newRelativeX=0,e(this).css(c.removeTransition());var n=e(this).position();u.relativePos=n.left,u.offsetX=i(t).x-n.left,u.offsetY=i(t).y-n.top,a("on"),u.sliding=!1,u.targetElement=t.target||t.srcElement}}function r(n){var n=n.originalEvent||n||l.event;c.newPosX=i(n).x-u.offsetX,c.newPosY=i(n).y-u.offsetY,c.newRelativeX=c.newPosX-u.relativePos,"function"==typeof c.options.startDragging&&u.dragging!==!0&&0!==c.newRelativeX&&(u.dragging=!0,c.options.startDragging.apply(c,[c.$elem])),(c.newRelativeX>8||c.newRelativeX<-8&&c.browser.isTouch===!0)&&(n.preventDefault?n.preventDefault():n.returnValue=!1,u.sliding=!0),(c.newPosY>10||c.newPosY<-10)&&u.sliding===!1&&e(t).off("touchmove.owl");var a=function(){return c.newRelativeX/5},o=function(){return c.maximumPixels+c.newRelativeX/5};c.newPosX=Math.max(Math.min(c.newPosX,a()),o()),c.browser.support3d===!0?c.transition3d(c.newPosX):c.css2move(c.newPosX)}function s(t){var t=t.originalEvent||t||l.event;if(t.target=t.target||t.srcElement,u.dragging=!1,c.browser.isTouch!==!0&&c.$owlWrapper.removeClass("grabbing"),c.dragDirection=c.owl.dragDirection=c.newRelativeX<0?"left":"right",0!==c.newRelativeX){var n=c.getNewPosition();if(c.goTo(n,!1,"drag"),u.targetElement===t.target&&c.browser.isTouch!==!0){e(t.target).on("click.disable",function(l){l.stopImmediatePropagation(),l.stopPropagation(),l.preventDefault(),e(t.target).off("click.disable")});var i=e._data(t.target,"events").click,o=i.pop();i.splice(0,0,o)}}a("off")}var c=this,u={offsetX:0,offsetY:0,baseElWidth:0,relativePos:0,position:null,minSwipe:null,maxSwipe:null,sliding:null,dargging:null,targetElement:null};c.isCssFinish=!0,c.$elem.on(c.ev_types.start,".owl-wrapper",o)},getNewPosition:function(){var e,l=this;return e=l.closestItem(),e>l.maximumItem?(l.currentItem=l.maximumItem,e=l.maximumItem):l.newPosX>=0&&(e=0,l.currentItem=0),e},closestItem:function(){var l=this,t=l.options.scrollPerPage===!0?l.pagesInArray:l.positionsInArray,n=l.newPosX,i=null;return e.each(t,function(a,o){n-l.itemWidth/20>t[a+1]&&n-l.itemWidth/20<o&&"left"===l.moveDirection()?(i=o,l.currentItem=l.options.scrollPerPage===!0?e.inArray(i,l.positionsInArray):a):n+l.itemWidth/20<o&&n+l.itemWidth/20>(t[a+1]||t[a]-l.itemWidth)&&"right"===l.moveDirection()&&(l.options.scrollPerPage===!0?(i=t[a+1]||t[t.length-1],l.currentItem=e.inArray(i,l.positionsInArray)):(i=t[a+1],l.currentItem=a+1))}),l.currentItem},moveDirection:function(){var e,l=this;return l.newRelativeX<0?(e="right",l.playDirection="next"):(e="left",l.playDirection="prev"),e},customEvents:function(){var e=this;e.$elem.on("owl.next",function(){e.next()}),e.$elem.on("owl.prev",function(){e.prev()}),e.$elem.on("owl.play",function(l,t){e.options.autoPlay=t,e.play(),e.hoverStatus="play"}),e.$elem.on("owl.stop",function(){e.stop(),e.hoverStatus="stop"}),e.$elem.on("owl.goTo",function(l,t){e.goTo(t)}),e.$elem.on("owl.jumpTo",function(l,t){e.jumpTo(t)})},stopOnHover:function(){var e=this;e.options.stopOnHover===!0&&e.browser.isTouch!==!0&&e.options.autoPlay!==!1&&(e.$elem.on("mouseover",function(){e.stop()}),e.$elem.on("mouseout",function(){"stop"!==e.hoverStatus&&e.play()}))},lazyLoad:function(){var l=this;if(l.options.lazyLoad===!1)return!1;for(var t=0;t<l.itemsAmount;t++){var i=e(l.$owlItems[t]);if("loaded"!==i.data("owl-loaded")){var a,o=i.data("owl-item"),r=i.find(".lazyOwl");"string"==typeof r.data("src")?(i.data("owl-loaded")===n&&(r.hide(),i.addClass("loading").data("owl-loaded","checked")),a=l.options.lazyFollow===!0?o>=l.currentItem:!0,a&&o<l.currentItem+l.options.items&&r.length&&l.lazyPreload(i,r)):i.data("owl-loaded","loaded")}}},lazyPreload:function(e,l){function t(){a+=1,i.completeImg(l.get(0))||o===!0?n():100>=a?setTimeout(t,100):n()}function n(){e.data("owl-loaded","loaded").removeClass("loading"),l.removeAttr("data-src"),"fade"===i.options.lazyEffect?l.fadeIn(400):l.show(),"function"==typeof i.options.afterLazyLoad&&i.options.afterLazyLoad.apply(this,[i.$elem])}var i=this,a=0;if("DIV"===l.prop("tagName")){l.css("background-image","url("+l.data("src")+")");var o=!0}else l[0].src=l.data("src");t()},autoHeight:function(){function l(){o+=1,i.completeImg(a.get(0))?t():100>=o?setTimeout(l,100):i.wrapperOuter.css("height","")}function t(){var l=e(i.$owlItems[i.currentItem]).height();i.wrapperOuter.css("height",l+"px"),i.wrapperOuter.hasClass("autoHeight")||setTimeout(function(){i.wrapperOuter.addClass("autoHeight")},0)}var i=this,a=e(i.$owlItems[i.currentItem]).find("img");if(a.get(0)!==n){var o=0;l()}else t()},completeImg:function(e){return e.complete?"undefined"!=typeof e.naturalWidth&&0==e.naturalWidth?!1:!0:!1},onVisibleItems:function(){var l=this;l.options.addClassActive===!0&&l.$owlItems.removeClass("active"),l.visibleItems=[];for(var t=l.currentItem;t<l.currentItem+l.options.items;t++)l.visibleItems.push(t),l.options.addClassActive===!0&&e(l.$owlItems[t]).addClass("active");l.owl.visibleItems=l.visibleItems},transitionTypes:function(e){var l=this;l.outClass="owl-"+e+"-out",l.inClass="owl-"+e+"-in"},singleItemTransition:function(){function e(e){return{position:"relative",left:e+"px"}}var l=this;l.isTransition=!0;var t=l.outClass,n=l.inClass,i=l.$owlItems.eq(l.currentItem),a=l.$owlItems.eq(l.prevItem),o=Math.abs(l.positionsInArray[l.currentItem])+l.positionsInArray[l.prevItem],r=Math.abs(l.positionsInArray[l.currentItem])+l.itemWidth/2;l.$owlWrapper.addClass("owl-origin").css({"-webkit-transform-origin":r+"px","-moz-perspective-origin":r+"px","perspective-origin":r+"px"});var s="webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend";a.css(e(o,10)).addClass(t).on(s,function(){l.endPrev=!0,a.off(s),l.clearTransStyle(a,t)}),i.addClass(n).on(s,function(){l.endCurrent=!0,i.off(s),l.clearTransStyle(i,n)})},clearTransStyle:function(e,l){var t=this;e.css({position:"",left:""}).removeClass(l),t.endPrev&&t.endCurrent&&(t.$owlWrapper.removeClass("owl-origin"),t.endPrev=!1,t.endCurrent=!1,t.isTransition=!1)},owlStatus:function(){var e=this;e.owl={userOptions:e.userOptions,baseElement:e.$elem,userItems:e.$userItems,owlItems:e.$owlItems,currentItem:e.currentItem,prevItem:e.prevItem,visibleItems:e.visibleItems,isTouch:e.browser.isTouch,browser:e.browser,dragDirection:e.dragDirection}},clearEvents:function(){var n=this;n.$elem.off(".owl owl mousedown.disableTextSelect"),e(t).off(".owl owl"),e(l).off("resize",n.resizer)},unWrap:function(){var e=this;0!==e.$elem.children().length&&(e.$owlWrapper.unwrap(),e.$userItems.unwrap().unwrap(),e.owlControls&&e.owlControls.remove()),e.clearEvents(),e.$elem.attr("style",e.$elem.data("owl-originalStyles")||"").attr("class",e.$elem.data("owl-originalClasses"))},destroy:function(){var e=this;e.stop(),clearInterval(e.checkVisible),e.unWrap(),e.$elem.removeData()},reinit:function(l){var t=this,n=e.extend({},t.userOptions,l);t.unWrap(),t.init(n,t.$elem)},addItem:function(e,l){var t,i=this;return e?0===i.$elem.children().length?(i.$elem.append(e),i.setVars(),!1):(i.unWrap(),t=l===n||-1===l?-1:l,t>=i.$userItems.length||-1===t?i.$userItems.eq(-1).after(e):i.$userItems.eq(t).before(e),i.setVars(),void 0):!1},removeItem:function(e){var l,t=this;return 0===t.$elem.children().length?!1:(l=e===n||-1===e?-1:e,t.unWrap(),t.$userItems.eq(l).remove(),t.setVars(),void 0)}};e.fn.owlCarousel=function(l){return this.each(function(){if(e(this).data("owl-init")===!0)return!1;e(this).data("owl-init",!0);var t=Object.create(i);t.init(l,this),e.data(this,"owlCarousel",t)})},e.fn.owlCarousel.options={items:5,itemsCustom:!1,itemsDesktop:[1199,4],itemsDesktopSmall:[979,3],itemsTablet:[768,2],itemsTabletSmall:!1,itemsMobile:[479,1],singleItem:!1,itemsScaleUp:!1,slideSpeed:200,paginationSpeed:800,rewindSpeed:1e3,autoPlay:!1,stopOnHover:!1,navigation:!1,navigationText:["prev","next"],rewindNav:!0,scrollPerPage:!1,pagination:!0,paginationNumbers:!1,responsive:!0,responsiveRefreshRate:200,responsiveBaseWidth:l,baseClass:"owl-carousel",theme:"owl-theme",lazyLoad:!1,lazyFollow:!0,lazyEffect:"fade",autoHeight:!1,jsonPath:!1,jsonSuccess:!1,dragBeforeAnimFinish:!0,mouseDrag:!0,touchDrag:!0,addClassActive:!1,transitionStyle:!1,beforeUpdate:!1,afterUpdate:!1,beforeInit:!1,afterInit:!1,beforeMove:!1,afterMove:!1,afterAction:!1,startDragging:!1,afterLazyLoad:!1}}(jQuery,window,document);