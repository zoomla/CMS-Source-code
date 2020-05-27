/* jpeg_encoder_basic.js  for android jpeg压缩质量修复 */
function JPEGEncoder(l) { var o = this; var s = Math.round; var k = Math.floor; var O = new Array(64); var K = new Array(64); var d = new Array(64); var Z = new Array(64); var u; var h; var G; var T; var n = new Array(65535); var m = new Array(65535); var P = new Array(64); var S = new Array(64); var j = []; var t = 0; var a = 7; var A = new Array(64); var f = new Array(64); var U = new Array(64); var e = new Array(256); var C = new Array(2048); var x; var i = [0, 1, 5, 6, 14, 15, 27, 28, 2, 4, 7, 13, 16, 26, 29, 42, 3, 8, 12, 17, 25, 30, 41, 43, 9, 11, 18, 24, 31, 40, 44, 53, 10, 19, 23, 32, 39, 45, 52, 54, 20, 22, 33, 38, 46, 51, 55, 60, 21, 34, 37, 47, 50, 56, 59, 61, 35, 36, 48, 49, 57, 58, 62, 63]; var g = [0, 0, 1, 5, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0]; var c = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]; var w = [0, 0, 2, 1, 3, 3, 2, 4, 3, 5, 5, 4, 4, 0, 0, 1, 125]; var E = [1, 2, 3, 0, 4, 17, 5, 18, 33, 49, 65, 6, 19, 81, 97, 7, 34, 113, 20, 50, 129, 145, 161, 8, 35, 66, 177, 193, 21, 82, 209, 240, 36, 51, 98, 114, 130, 9, 10, 22, 23, 24, 25, 26, 37, 38, 39, 40, 41, 42, 52, 53, 54, 55, 56, 57, 58, 67, 68, 69, 70, 71, 72, 73, 74, 83, 84, 85, 86, 87, 88, 89, 90, 99, 100, 101, 102, 103, 104, 105, 106, 115, 116, 117, 118, 119, 120, 121, 122, 131, 132, 133, 134, 135, 136, 137, 138, 146, 147, 148, 149, 150, 151, 152, 153, 154, 162, 163, 164, 165, 166, 167, 168, 169, 170, 178, 179, 180, 181, 182, 183, 184, 185, 186, 194, 195, 196, 197, 198, 199, 200, 201, 202, 210, 211, 212, 213, 214, 215, 216, 217, 218, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250]; var v = [0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0]; var Y = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]; var J = [0, 0, 2, 1, 2, 4, 4, 3, 4, 7, 5, 4, 4, 0, 1, 2, 119]; var B = [0, 1, 2, 3, 17, 4, 5, 33, 49, 6, 18, 65, 81, 7, 97, 113, 19, 34, 50, 129, 8, 20, 66, 145, 161, 177, 193, 9, 35, 51, 82, 240, 21, 98, 114, 209, 10, 22, 36, 52, 225, 37, 241, 23, 24, 25, 26, 38, 39, 40, 41, 42, 53, 54, 55, 56, 57, 58, 67, 68, 69, 70, 71, 72, 73, 74, 83, 84, 85, 86, 87, 88, 89, 90, 99, 100, 101, 102, 103, 104, 105, 106, 115, 116, 117, 118, 119, 120, 121, 122, 130, 131, 132, 133, 134, 135, 136, 137, 138, 146, 147, 148, 149, 150, 151, 152, 153, 154, 162, 163, 164, 165, 166, 167, 168, 169, 170, 178, 179, 180, 181, 182, 183, 184, 185, 186, 194, 195, 196, 197, 198, 199, 200, 201, 202, 210, 211, 212, 213, 214, 215, 216, 217, 218, 226, 227, 228, 229, 230, 231, 232, 233, 234, 242, 243, 244, 245, 246, 247, 248, 249, 250]; function M(ag) { var af = [16, 11, 10, 16, 24, 40, 51, 61, 12, 12, 14, 19, 26, 58, 60, 55, 14, 13, 16, 24, 40, 57, 69, 56, 14, 17, 22, 29, 51, 87, 80, 62, 18, 22, 37, 56, 68, 109, 103, 77, 24, 35, 55, 64, 81, 104, 113, 92, 49, 64, 78, 87, 103, 121, 120, 101, 72, 92, 95, 98, 112, 100, 103, 99]; for (var ae = 0; ae < 64; ae++) { var aj = k((af[ae] * ag + 50) / 100); if (aj < 1) { aj = 1 } else { if (aj > 255) { aj = 255 } } O[i[ae]] = aj } var ah = [17, 18, 24, 47, 99, 99, 99, 99, 18, 21, 26, 66, 99, 99, 99, 99, 24, 26, 56, 99, 99, 99, 99, 99, 47, 66, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99, 99]; for (var ad = 0; ad < 64; ad++) { var ai = k((ah[ad] * ag + 50) / 100); if (ai < 1) { ai = 1 } else { if (ai > 255) { ai = 255 } } K[i[ad]] = ai } var ac = [1, 1.387039845, 1.306562965, 1.175875602, 1, 0.785694958, 0.5411961, 0.275899379]; var ab = 0; for (var ak = 0; ak < 8; ak++) { for (var aa = 0; aa < 8; aa++) { d[ab] = (1 / (O[i[ab]] * ac[ak] * ac[aa] * 8)); Z[ab] = (1 / (K[i[ab]] * ac[ak] * ac[aa] * 8)); ab++ } } } function q(ae, aa) { var ad = 0; var ag = 0; var af = new Array(); for (var ab = 1; ab <= 16; ab++) { for (var ac = 1; ac <= ae[ab]; ac++) { af[aa[ag]] = []; af[aa[ag]][0] = ad; af[aa[ag]][1] = ab; ag++; ad++ } ad *= 2 } return af } function W() { u = q(g, c); h = q(v, Y); G = q(w, E); T = q(J, B) } function z() { var ac = 1; var ab = 2; for (var aa = 1; aa <= 15; aa++) { for (var ad = ac; ad < ab; ad++) { m[32767 + ad] = aa; n[32767 + ad] = []; n[32767 + ad][1] = aa; n[32767 + ad][0] = ad } for (var ae = -(ab - 1) ; ae <= -ac; ae++) { m[32767 + ae] = aa; n[32767 + ae] = []; n[32767 + ae][1] = aa; n[32767 + ae][0] = ab - 1 + ae } ac <<= 1; ab <<= 1 } } function V() { for (var aa = 0; aa < 256; aa++) { C[aa] = 19595 * aa; C[(aa + 256) >> 0] = 38470 * aa; C[(aa + 512) >> 0] = 7471 * aa + 32768; C[(aa + 768) >> 0] = -11059 * aa; C[(aa + 1024) >> 0] = -21709 * aa; C[(aa + 1280) >> 0] = 32768 * aa + 8421375; C[(aa + 1536) >> 0] = -27439 * aa; C[(aa + 1792) >> 0] = -5329 * aa } } function X(aa) { var ac = aa[0]; var ab = aa[1] - 1; while (ab >= 0) { if (ac & (1 << ab)) { t |= (1 << a) } ab--; a--; if (a < 0) { if (t == 255) { F(255); F(0) } else { F(t) } a = 7; t = 0 } } } function F(aa) { j.push(e[aa]) } function p(aa) { F((aa >> 8) & 255); F((aa) & 255) } function N(aZ, ap) { var aL, aK, aJ, aI, aH, aD, aC, aB; var aN = 0; var aR; const aq = 8; const ai = 64; for (aR = 0; aR < aq; ++aR) { aL = aZ[aN]; aK = aZ[aN + 1]; aJ = aZ[aN + 2]; aI = aZ[aN + 3]; aH = aZ[aN + 4]; aD = aZ[aN + 5]; aC = aZ[aN + 6]; aB = aZ[aN + 7]; var aY = aL + aB; var aO = aL - aB; var aX = aK + aC; var aP = aK - aC; var aU = aJ + aD; var aQ = aJ - aD; var aT = aI + aH; var aS = aI - aH; var an = aY + aT; var ak = aY - aT; var am = aX + aU; var al = aX - aU; aZ[aN] = an + am; aZ[aN + 4] = an - am; var ax = (al + ak) * 0.707106781; aZ[aN + 2] = ak + ax; aZ[aN + 6] = ak - ax; an = aS + aQ; am = aQ + aP; al = aP + aO; var at = (an - al) * 0.382683433; var aw = 0.5411961 * an + at; var au = 1.306562965 * al + at; var av = am * 0.707106781; var ah = aO + av; var ag = aO - av; aZ[aN + 5] = ag + aw; aZ[aN + 3] = ag - aw; aZ[aN + 1] = ah + au; aZ[aN + 7] = ah - au; aN += 8 } aN = 0; for (aR = 0; aR < aq; ++aR) { aL = aZ[aN]; aK = aZ[aN + 8]; aJ = aZ[aN + 16]; aI = aZ[aN + 24]; aH = aZ[aN + 32]; aD = aZ[aN + 40]; aC = aZ[aN + 48]; aB = aZ[aN + 56]; var ar = aL + aB; var aj = aL - aB; var az = aK + aC; var ae = aK - aC; var aG = aJ + aD; var ac = aJ - aD; var aW = aI + aH; var aa = aI - aH; var ao = ar + aW; var aV = ar - aW; var ay = az + aG; var aF = az - aG; aZ[aN] = ao + ay; aZ[aN + 32] = ao - ay; var af = (aF + aV) * 0.707106781; aZ[aN + 16] = aV + af; aZ[aN + 48] = aV - af; ao = aa + ac; ay = ac + ae; aF = ae + aj; var aM = (ao - aF) * 0.382683433; var ad = 0.5411961 * ao + aM; var a1 = 1.306562965 * aF + aM; var ab = ay * 0.707106781; var a0 = aj + ab; var aA = aj - ab; aZ[aN + 40] = aA + ad; aZ[aN + 24] = aA - ad; aZ[aN + 8] = a0 + a1; aZ[aN + 56] = a0 - a1; aN++ } var aE; for (aR = 0; aR < ai; ++aR) { aE = aZ[aR] * ap[aR]; P[aR] = (aE > 0) ? ((aE + 0.5) | 0) : ((aE - 0.5) | 0) } return P } function b() { p(65504); p(16); F(74); F(70); F(73); F(70); F(0); F(1); F(1); F(0); p(1); p(1); F(0); F(0) } function r(aa, ab) { p(65472); p(17); F(8); p(ab); p(aa); F(3); F(1); F(17); F(0); F(2); F(17); F(1); F(3); F(17); F(1) } function D() { p(65499); p(132); F(0); for (var ab = 0; ab < 64; ab++) { F(O[ab]) } F(1); for (var aa = 0; aa < 64; aa++) { F(K[aa]) } } function H() { p(65476); p(418); F(0); for (var ae = 0; ae < 16; ae++) { F(g[ae + 1]) } for (var ad = 0; ad <= 11; ad++) { F(c[ad]) } F(16); for (var ac = 0; ac < 16; ac++) { F(w[ac + 1]) } for (var ab = 0; ab <= 161; ab++) { F(E[ab]) } F(1); for (var aa = 0; aa < 16; aa++) { F(v[aa + 1]) } for (var ah = 0; ah <= 11; ah++) { F(Y[ah]) } F(17); for (var ag = 0; ag < 16; ag++) { F(J[ag + 1]) } for (var af = 0; af <= 161; af++) { F(B[af]) } } function I() { p(65498); p(12); F(3); F(1); F(0); F(2); F(17); F(3); F(17); F(0); F(63); F(0) } function L(ad, aa, al, at, ap) { var ag = ap[0]; var ab = ap[240]; var ac; const ar = 16; const ai = 63; const ah = 64; var aq = N(ad, aa); for (var am = 0; am < ah; ++am) { S[i[am]] = aq[am] } var an = S[0] - al; al = S[0]; if (an == 0) { X(at[0]) } else { ac = 32767 + an; X(at[m[ac]]); X(n[ac]) } var ae = 63; for (; (ae > 0) && (S[ae] == 0) ; ae--) { } if (ae == 0) { X(ag); return al } var ao = 1; var au; while (ao <= ae) { var ak = ao; for (; (S[ao] == 0) && (ao <= ae) ; ++ao) { } var aj = ao - ak; if (aj >= ar) { au = aj >> 4; for (var af = 1; af <= au; ++af) { X(ab) } aj = aj & 15 } ac = 32767 + S[ao]; X(ap[(aj << 4) + m[ac]]); X(n[ac]); ao++ } if (ae != ai) { X(ag) } return al } function y() { var ab = String.fromCharCode; for (var aa = 0; aa < 256; aa++) { e[aa] = ab(aa) } } this.encode = function (an, aj, aB) { var aa = new Date().getTime(); if (aj) { R(aj) } j = new Array(); t = 0; a = 7; p(65496); b(); D(); r(an.width, an.height); H(); I(); var al = 0; var aq = 0; var ao = 0; t = 0; a = 7; this.encode.displayName = "_encode_"; var at = an.data; var ar = an.width; var aA = an.height; var ay = ar * 4; var ai = ar * 3; var ah, ag = 0; var am, ax, az; var ab, ap, ac, af, ae; while (ag < aA) { ah = 0; while (ah < ay) { ab = ay * ag + ah; ap = ab; ac = -1; af = 0; for (ae = 0; ae < 64; ae++) { af = ae >> 3; ac = (ae & 7) * 4; ap = ab + (af * ay) + ac; if (ag + af >= aA) { ap -= (ay * (ag + 1 + af - aA)) } if (ah + ac >= ay) { ap -= ((ah + ac) - ay + 4) } am = at[ap++]; ax = at[ap++]; az = at[ap++]; A[ae] = ((C[am] + C[(ax + 256) >> 0] + C[(az + 512) >> 0]) >> 16) - 128; f[ae] = ((C[(am + 768) >> 0] + C[(ax + 1024) >> 0] + C[(az + 1280) >> 0]) >> 16) - 128; U[ae] = ((C[(am + 1280) >> 0] + C[(ax + 1536) >> 0] + C[(az + 1792) >> 0]) >> 16) - 128 } al = L(A, d, al, u, G); aq = L(f, Z, aq, h, T); ao = L(U, Z, ao, h, T); ah += 32 } ag += 8 } if (a >= 0) { var aw = []; aw[1] = a + 1; aw[0] = (1 << (a + 1)) - 1; X(aw) } p(65497); if (aB) { var av = j.length; var aC = new Uint8Array(av); for (var au = 0; au < av; au++) { aC[au] = j[au].charCodeAt() } j = []; var ak = new Date().getTime() - aa; console.log("Encoding time: " + ak + "ms"); return aC } var ad = "data:image/jpeg;base64," + btoa(j.join("")); j = []; var ak = new Date().getTime() - aa; console.log("Encoding time: " + ak + "ms"); return ad }; function R(ab) { if (ab <= 0) { ab = 1 } if (ab > 100) { ab = 100 } if (x == ab) { return } var aa = 0; if (ab < 50) { aa = Math.floor(5000 / ab) } else { aa = Math.floor(200 - ab * 2) } M(aa); x = ab; console.log("Quality set to: " + ab + "%") } function Q() { var aa = new Date().getTime(); if (!l) { l = 50 } y(); W(); z(); V(); R(l); var ab = new Date().getTime() - aa; console.log("Initialization " + ab + "ms") } Q() };

/* megapix-image.js for IOS(iphone5+) drawImage画面扭曲修复  */
/**
 * [description]
 * @param  {[type]} ){function                                                                                                        [description]
 */
!function () { function a(a) { var d, e, b = a.naturalWidth, c = a.naturalHeight; return b * c > 1048576 ? (d = document.createElement("canvas"), d.width = d.height = 1, e = d.getContext("2d"), e.drawImage(a, -b + 1, 0), 0 === e.getImageData(0, 0, 1, 1).data[3]) : !1 } function b(a, b, c) { var e, f, g, h, i, j, k, d = document.createElement("canvas"); for (d.width = 1, d.height = c, e = d.getContext("2d"), e.drawImage(a, 0, 0), f = e.getImageData(0, 0, 1, c).data, g = 0, h = c, i = c; i > g;) j = f[4 * (i - 1) + 3], 0 === j ? h = i : g = i, i = h + g >> 1; return k = i / c, 0 === k ? 1 : k } function c(a, b, c) { var e = document.createElement("canvas"); return d(a, e, b, c), e.toDataURL("image/jpeg", b.quality || .8) } function d(c, d, f, g) { var m, n, o, p, q, r, s, t, u, v, w, h = c.naturalWidth, i = c.naturalHeight, j = f.width, k = f.height, l = d.getContext("2d"); for (l.save(), e(d, l, j, k, f.orientation), m = a(c), m && (h /= 2, i /= 2), n = 1024, o = document.createElement("canvas"), o.width = o.height = n, p = o.getContext("2d"), q = g ? b(c, h, i) : 1, r = Math.ceil(n * j / h), s = Math.ceil(n * k / i / q), t = 0, u = 0; i > t;) { for (v = 0, w = 0; h > v;) p.clearRect(0, 0, n, n), p.drawImage(c, -v, -t), l.drawImage(o, 0, 0, n, n, w, u, r, s), v += n, w += r; t += n, u += s } l.restore(), o = p = null } function e(a, b, c, d, e) { switch (e) { case 5: case 6: case 7: case 8: a.width = d, a.height = c; break; default: a.width = c, a.height = d } switch (e) { case 2: b.translate(c, 0), b.scale(-1, 1); break; case 3: b.translate(c, d), b.rotate(Math.PI); break; case 4: b.translate(0, d), b.scale(1, -1); break; case 5: b.rotate(.5 * Math.PI), b.scale(1, -1); break; case 6: b.rotate(.5 * Math.PI), b.translate(0, -d); break; case 7: b.rotate(.5 * Math.PI), b.translate(c, -d), b.scale(-1, 1); break; case 8: b.rotate(-.5 * Math.PI), b.translate(-c, 0) } } function f(a) { var b, c, d; if (window.Blob && a instanceof Blob) { if (b = new Image, c = window.URL && window.URL.createObjectURL ? window.URL : window.webkitURL && window.webkitURL.createObjectURL ? window.webkitURL : null, !c) throw Error("No createObjectURL function found to create blob url"); b.src = c.createObjectURL(a), this.blob = a, a = b } a.naturalWidth || a.naturalHeight || (d = this, a.onload = function () { var b, c, a = d.imageLoadListeners; if (a) for (d.imageLoadListeners = null, b = 0, c = a.length; c > b; b++) a[b]() }, this.imageLoadListeners = []), this.srcImage = a } f.prototype.render = function (a, b, e) { var f, g, h, i, j, k, l, m, n, o, p; if (this.imageLoadListeners) return f = this, this.imageLoadListeners.push(function () { f.render(a, b, e) }), void 0; b = b || {}, g = this.srcImage.naturalWidth, h = this.srcImage.naturalHeight, i = b.width, j = b.height, k = b.maxWidth, l = b.maxHeight, m = !this.blob || "image/jpeg" === this.blob.type, i && !j ? j = h * i / g << 0 : j && !i ? i = g * j / h << 0 : (i = g, j = h), k && i > k && (i = k, j = h * i / g << 0), l && j > l && (j = l, i = g * j / h << 0), n = { width: i, height: j }; for (o in b) n[o] = b[o]; p = a.tagName.toLowerCase(), "img" === p ? a.src = c(this.srcImage, n, m) : "canvas" === p && d(this.srcImage, a, n, m), "function" == typeof this.onrender && this.onrender(a), e && e() }, "function" == typeof define && define.amd ? define([], function () { return f }) : this.MegaPixImage = f }();

//exif.js
(function () {

    var debug = false;

    var root = this;

    var EXIF = function (obj) {
        if (obj instanceof EXIF) return obj;
        if (!(this instanceof EXIF)) return new EXIF(obj);
        this.EXIFwrapped = obj;
    };

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = EXIF;
        }
        exports.EXIF = EXIF;
    } else {
        root.EXIF = EXIF;
    }

    var ExifTags = EXIF.Tags = {

        // version tags
        0x9000: "ExifVersion",             // EXIF version
        0xA000: "FlashpixVersion",         // Flashpix format version

        // colorspace tags
        0xA001: "ColorSpace",              // Color space information tag

        // image configuration
        0xA002: "PixelXDimension",         // Valid width of meaningful image
        0xA003: "PixelYDimension",         // Valid height of meaningful image
        0x9101: "ComponentsConfiguration", // Information about channels
        0x9102: "CompressedBitsPerPixel",  // Compressed bits per pixel

        // user information
        0x927C: "MakerNote",               // Any desired information written by the manufacturer
        0x9286: "UserComment",             // Comments by user

        // related file
        0xA004: "RelatedSoundFile",        // Name of related sound file

        // date and time
        0x9003: "DateTimeOriginal",        // Date and time when the original image was generated
        0x9004: "DateTimeDigitized",       // Date and time when the image was stored digitally
        0x9290: "SubsecTime",              // Fractions of seconds for DateTime
        0x9291: "SubsecTimeOriginal",      // Fractions of seconds for DateTimeOriginal
        0x9292: "SubsecTimeDigitized",     // Fractions of seconds for DateTimeDigitized

        // picture-taking conditions
        0x829A: "ExposureTime",            // Exposure time (in seconds)
        0x829D: "FNumber",                 // F number
        0x8822: "ExposureProgram",         // Exposure program
        0x8824: "SpectralSensitivity",     // Spectral sensitivity
        0x8827: "ISOSpeedRatings",         // ISO speed rating
        0x8828: "OECF",                    // Optoelectric conversion factor
        0x9201: "ShutterSpeedValue",       // Shutter speed
        0x9202: "ApertureValue",           // Lens aperture
        0x9203: "BrightnessValue",         // Value of brightness
        0x9204: "ExposureBias",            // Exposure bias
        0x9205: "MaxApertureValue",        // Smallest F number of lens
        0x9206: "SubjectDistance",         // Distance to subject in meters
        0x9207: "MeteringMode",            // Metering mode
        0x9208: "LightSource",             // Kind of light source
        0x9209: "Flash",                   // Flash status
        0x9214: "SubjectArea",             // Location and area of main subject
        0x920A: "FocalLength",             // Focal length of the lens in mm
        0xA20B: "FlashEnergy",             // Strobe energy in BCPS
        0xA20C: "SpatialFrequencyResponse",    //
        0xA20E: "FocalPlaneXResolution",   // Number of pixels in width direction per FocalPlaneResolutionUnit
        0xA20F: "FocalPlaneYResolution",   // Number of pixels in height direction per FocalPlaneResolutionUnit
        0xA210: "FocalPlaneResolutionUnit",    // Unit for measuring FocalPlaneXResolution and FocalPlaneYResolution
        0xA214: "SubjectLocation",         // Location of subject in image
        0xA215: "ExposureIndex",           // Exposure index selected on camera
        0xA217: "SensingMethod",           // Image sensor type
        0xA300: "FileSource",              // Image source (3 == DSC)
        0xA301: "SceneType",               // Scene type (1 == directly photographed)
        0xA302: "CFAPattern",              // Color filter array geometric pattern
        0xA401: "CustomRendered",          // Special processing
        0xA402: "ExposureMode",            // Exposure mode
        0xA403: "WhiteBalance",            // 1 = auto white balance, 2 = manual
        0xA404: "DigitalZoomRation",       // Digital zoom ratio
        0xA405: "FocalLengthIn35mmFilm",   // Equivalent foacl length assuming 35mm film camera (in mm)
        0xA406: "SceneCaptureType",        // Type of scene
        0xA407: "GainControl",             // Degree of overall image gain adjustment
        0xA408: "Contrast",                // Direction of contrast processing applied by camera
        0xA409: "Saturation",              // Direction of saturation processing applied by camera
        0xA40A: "Sharpness",               // Direction of sharpness processing applied by camera
        0xA40B: "DeviceSettingDescription",    //
        0xA40C: "SubjectDistanceRange",    // Distance to subject

        // other tags
        0xA005: "InteroperabilityIFDPointer",
        0xA420: "ImageUniqueID"            // Identifier assigned uniquely to each image
    };

    var TiffTags = EXIF.TiffTags = {
        0x0100: "ImageWidth",
        0x0101: "ImageHeight",
        0x8769: "ExifIFDPointer",
        0x8825: "GPSInfoIFDPointer",
        0xA005: "InteroperabilityIFDPointer",
        0x0102: "BitsPerSample",
        0x0103: "Compression",
        0x0106: "PhotometricInterpretation",
        0x0112: "Orientation",
        0x0115: "SamplesPerPixel",
        0x011C: "PlanarConfiguration",
        0x0212: "YCbCrSubSampling",
        0x0213: "YCbCrPositioning",
        0x011A: "XResolution",
        0x011B: "YResolution",
        0x0128: "ResolutionUnit",
        0x0111: "StripOffsets",
        0x0116: "RowsPerStrip",
        0x0117: "StripByteCounts",
        0x0201: "JPEGInterchangeFormat",
        0x0202: "JPEGInterchangeFormatLength",
        0x012D: "TransferFunction",
        0x013E: "WhitePoint",
        0x013F: "PrimaryChromaticities",
        0x0211: "YCbCrCoefficients",
        0x0214: "ReferenceBlackWhite",
        0x0132: "DateTime",
        0x010E: "ImageDescription",
        0x010F: "Make",
        0x0110: "Model",
        0x0131: "Software",
        0x013B: "Artist",
        0x8298: "Copyright"
    };

    var GPSTags = EXIF.GPSTags = {
        0x0000: "GPSVersionID",
        0x0001: "GPSLatitudeRef",
        0x0002: "GPSLatitude",
        0x0003: "GPSLongitudeRef",
        0x0004: "GPSLongitude",
        0x0005: "GPSAltitudeRef",
        0x0006: "GPSAltitude",
        0x0007: "GPSTimeStamp",
        0x0008: "GPSSatellites",
        0x0009: "GPSStatus",
        0x000A: "GPSMeasureMode",
        0x000B: "GPSDOP",
        0x000C: "GPSSpeedRef",
        0x000D: "GPSSpeed",
        0x000E: "GPSTrackRef",
        0x000F: "GPSTrack",
        0x0010: "GPSImgDirectionRef",
        0x0011: "GPSImgDirection",
        0x0012: "GPSMapDatum",
        0x0013: "GPSDestLatitudeRef",
        0x0014: "GPSDestLatitude",
        0x0015: "GPSDestLongitudeRef",
        0x0016: "GPSDestLongitude",
        0x0017: "GPSDestBearingRef",
        0x0018: "GPSDestBearing",
        0x0019: "GPSDestDistanceRef",
        0x001A: "GPSDestDistance",
        0x001B: "GPSProcessingMethod",
        0x001C: "GPSAreaInformation",
        0x001D: "GPSDateStamp",
        0x001E: "GPSDifferential"
    };

    var StringValues = EXIF.StringValues = {
        ExposureProgram: {
            0: "Not defined",
            1: "Manual",
            2: "Normal program",
            3: "Aperture priority",
            4: "Shutter priority",
            5: "Creative program",
            6: "Action program",
            7: "Portrait mode",
            8: "Landscape mode"
        },
        MeteringMode: {
            0: "Unknown",
            1: "Average",
            2: "CenterWeightedAverage",
            3: "Spot",
            4: "MultiSpot",
            5: "Pattern",
            6: "Partial",
            255: "Other"
        },
        LightSource: {
            0: "Unknown",
            1: "Daylight",
            2: "Fluorescent",
            3: "Tungsten (incandescent light)",
            4: "Flash",
            9: "Fine weather",
            10: "Cloudy weather",
            11: "Shade",
            12: "Daylight fluorescent (D 5700 - 7100K)",
            13: "Day white fluorescent (N 4600 - 5400K)",
            14: "Cool white fluorescent (W 3900 - 4500K)",
            15: "White fluorescent (WW 3200 - 3700K)",
            17: "Standard light A",
            18: "Standard light B",
            19: "Standard light C",
            20: "D55",
            21: "D65",
            22: "D75",
            23: "D50",
            24: "ISO studio tungsten",
            255: "Other"
        },
        Flash: {
            0x0000: "Flash did not fire",
            0x0001: "Flash fired",
            0x0005: "Strobe return light not detected",
            0x0007: "Strobe return light detected",
            0x0009: "Flash fired, compulsory flash mode",
            0x000D: "Flash fired, compulsory flash mode, return light not detected",
            0x000F: "Flash fired, compulsory flash mode, return light detected",
            0x0010: "Flash did not fire, compulsory flash mode",
            0x0018: "Flash did not fire, auto mode",
            0x0019: "Flash fired, auto mode",
            0x001D: "Flash fired, auto mode, return light not detected",
            0x001F: "Flash fired, auto mode, return light detected",
            0x0020: "No flash function",
            0x0041: "Flash fired, red-eye reduction mode",
            0x0045: "Flash fired, red-eye reduction mode, return light not detected",
            0x0047: "Flash fired, red-eye reduction mode, return light detected",
            0x0049: "Flash fired, compulsory flash mode, red-eye reduction mode",
            0x004D: "Flash fired, compulsory flash mode, red-eye reduction mode, return light not detected",
            0x004F: "Flash fired, compulsory flash mode, red-eye reduction mode, return light detected",
            0x0059: "Flash fired, auto mode, red-eye reduction mode",
            0x005D: "Flash fired, auto mode, return light not detected, red-eye reduction mode",
            0x005F: "Flash fired, auto mode, return light detected, red-eye reduction mode"
        },
        SensingMethod: {
            1: "Not defined",
            2: "One-chip color area sensor",
            3: "Two-chip color area sensor",
            4: "Three-chip color area sensor",
            5: "Color sequential area sensor",
            7: "Trilinear sensor",
            8: "Color sequential linear sensor"
        },
        SceneCaptureType: {
            0: "Standard",
            1: "Landscape",
            2: "Portrait",
            3: "Night scene"
        },
        SceneType: {
            1: "Directly photographed"
        },
        CustomRendered: {
            0: "Normal process",
            1: "Custom process"
        },
        WhiteBalance: {
            0: "Auto white balance",
            1: "Manual white balance"
        },
        GainControl: {
            0: "None",
            1: "Low gain up",
            2: "High gain up",
            3: "Low gain down",
            4: "High gain down"
        },
        Contrast: {
            0: "Normal",
            1: "Soft",
            2: "Hard"
        },
        Saturation: {
            0: "Normal",
            1: "Low saturation",
            2: "High saturation"
        },
        Sharpness: {
            0: "Normal",
            1: "Soft",
            2: "Hard"
        },
        SubjectDistanceRange: {
            0: "Unknown",
            1: "Macro",
            2: "Close view",
            3: "Distant view"
        },
        FileSource: {
            3: "DSC"
        },

        Components: {
            0: "",
            1: "Y",
            2: "Cb",
            3: "Cr",
            4: "R",
            5: "G",
            6: "B"
        }
    };

    function addEvent(element, event, handler) {
        if (element.addEventListener) {
            element.addEventListener(event, handler, false);
        } else if (element.attachEvent) {
            element.attachEvent("on" + event, handler);
        }
    }

    function imageHasData(img) {
        return !!(img.exifdata);
    }


    function base64ToArrayBuffer(base64, contentType) {
        contentType = contentType || base64.match(/^data\:([^\;]+)\;base64,/mi)[1] || ''; // e.g. 'data:image/jpeg;base64,...' => 'image/jpeg'
        base64 = base64.replace(/^data\:([^\;]+)\;base64,/gmi, '');
        var binary = atob(base64);
        var len = binary.length;
        var buffer = new ArrayBuffer(len);
        var view = new Uint8Array(buffer);
        for (var i = 0; i < len; i++) {
            view[i] = binary.charCodeAt(i);
        }
        return buffer;
    }

    function objectURLToBlob(url, callback) {
        var http = new XMLHttpRequest();
        http.open("GET", url, true);
        http.responseType = "blob";
        http.onload = function (e) {
            if (this.status == 200 || this.status === 0) {
                callback(this.response);
            }
        };
        http.send();
    }

    function getImageData(img, callback) {
        function handleBinaryFile(binFile) {
            var data = findEXIFinJPEG(binFile);
            var iptcdata = findIPTCinJPEG(binFile);
            img.exifdata = data || {};
            img.iptcdata = iptcdata || {};
            if (callback) {
                callback.call(img);
            }
        }

        if (img.src) {
            if (/^data\:/i.test(img.src)) { // Data URI
                var arrayBuffer = base64ToArrayBuffer(img.src);
                handleBinaryFile(arrayBuffer);

            } else if (/^blob\:/i.test(img.src)) { // Object URL
                var fileReader = new FileReader();
                fileReader.onload = function (e) {
                    handleBinaryFile(e.target.result);
                };
                objectURLToBlob(img.src, function (blob) {
                    fileReader.readAsArrayBuffer(blob);
                });
            } else {
                var http = new XMLHttpRequest();
                http.onload = function () {
                    if (this.status == 200 || this.status === 0) {
                        handleBinaryFile(http.response);
                    } else {
                        throw "Could not load image";
                    }
                    http = null;
                };
                http.open("GET", img.src, true);
                http.responseType = "arraybuffer";
                http.send(null);
            }
        } else if (window.FileReader && (img instanceof window.Blob || img instanceof window.File)) {
            var fileReader = new FileReader();
            fileReader.onload = function (e) {
                if (debug) console.log("Got file of length " + e.target.result.byteLength);
                handleBinaryFile(e.target.result);
            };

            fileReader.readAsArrayBuffer(img);
        }
    }

    function findEXIFinJPEG(file) {
        var dataView = new DataView(file);

        if (debug) console.log("Got file of length " + file.byteLength);
        if ((dataView.getUint8(0) != 0xFF) || (dataView.getUint8(1) != 0xD8)) {
            if (debug) console.log("Not a valid JPEG");
            return false; // not a valid jpeg
        }

        var offset = 2,
            length = file.byteLength,
            marker;

        while (offset < length) {
            if (dataView.getUint8(offset) != 0xFF) {
                if (debug) console.log("Not a valid marker at offset " + offset + ", found: " + dataView.getUint8(offset));
                return false; // not a valid marker, something is wrong
            }

            marker = dataView.getUint8(offset + 1);
            if (debug) console.log(marker);

            // we could implement handling for other markers here,
            // but we're only looking for 0xFFE1 for EXIF data

            if (marker == 225) {
                if (debug) console.log("Found 0xFFE1 marker");

                return readEXIFData(dataView, offset + 4, dataView.getUint16(offset + 2) - 2);

                // offset += 2 + file.getShortAt(offset+2, true);

            } else {
                offset += 2 + dataView.getUint16(offset + 2);
            }

        }

    }

    function findIPTCinJPEG(file) {
        var dataView = new DataView(file);

        if (debug) console.log("Got file of length " + file.byteLength);
        if ((dataView.getUint8(0) != 0xFF) || (dataView.getUint8(1) != 0xD8)) {
            if (debug) console.log("Not a valid JPEG");
            return false; // not a valid jpeg
        }

        var offset = 2,
            length = file.byteLength;


        var isFieldSegmentStart = function (dataView, offset) {
            return (
                dataView.getUint8(offset) === 0x38 &&
                dataView.getUint8(offset + 1) === 0x42 &&
                dataView.getUint8(offset + 2) === 0x49 &&
                dataView.getUint8(offset + 3) === 0x4D &&
                dataView.getUint8(offset + 4) === 0x04 &&
                dataView.getUint8(offset + 5) === 0x04
            );
        };

        while (offset < length) {

            if (isFieldSegmentStart(dataView, offset)) {

                // Get the length of the name header (which is padded to an even number of bytes)
                var nameHeaderLength = dataView.getUint8(offset + 7);
                if (nameHeaderLength % 2 !== 0) nameHeaderLength += 1;
                // Check for pre photoshop 6 format
                if (nameHeaderLength === 0) {
                    // Always 4
                    nameHeaderLength = 4;
                }

                var startOffset = offset + 8 + nameHeaderLength;
                var sectionLength = dataView.getUint16(offset + 6 + nameHeaderLength);

                return readIPTCData(file, startOffset, sectionLength);

                break;

            }


            // Not the marker, continue searching
            offset++;

        }

    }
    var IptcFieldMap = {
        0x78: 'caption',
        0x6E: 'credit',
        0x19: 'keywords',
        0x37: 'dateCreated',
        0x50: 'byline',
        0x55: 'bylineTitle',
        0x7A: 'captionWriter',
        0x69: 'headline',
        0x74: 'copyright',
        0x0F: 'category'
    };
    function readIPTCData(file, startOffset, sectionLength) {
        var dataView = new DataView(file);
        var data = {};
        var fieldValue, fieldName, dataSize, segmentType, segmentSize;
        var segmentStartPos = startOffset;
        while (segmentStartPos < startOffset + sectionLength) {
            if (dataView.getUint8(segmentStartPos) === 0x1C && dataView.getUint8(segmentStartPos + 1) === 0x02) {
                segmentType = dataView.getUint8(segmentStartPos + 2);
                if (segmentType in IptcFieldMap) {
                    dataSize = dataView.getInt16(segmentStartPos + 3);
                    segmentSize = dataSize + 5;
                    fieldName = IptcFieldMap[segmentType];
                    fieldValue = getStringFromDB(dataView, segmentStartPos + 5, dataSize);
                    // Check if we already stored a value with this name
                    if (data.hasOwnProperty(fieldName)) {
                        // Value already stored with this name, create multivalue field
                        if (data[fieldName] instanceof Array) {
                            data[fieldName].push(fieldValue);
                        }
                        else {
                            data[fieldName] = [data[fieldName], fieldValue];
                        }
                    }
                    else {
                        data[fieldName] = fieldValue;
                    }
                }

            }
            segmentStartPos++;
        }
        return data;
    }



    function readTags(file, tiffStart, dirStart, strings, bigEnd) {
        var entries = file.getUint16(dirStart, !bigEnd),
            tags = {},
            entryOffset, tag,
            i;

        for (i = 0; i < entries; i++) {
            entryOffset = dirStart + i * 12 + 2;
            tag = strings[file.getUint16(entryOffset, !bigEnd)];
            if (!tag && debug) console.log("Unknown tag: " + file.getUint16(entryOffset, !bigEnd));
            tags[tag] = readTagValue(file, entryOffset, tiffStart, dirStart, bigEnd);
        }
        return tags;
    }


    function readTagValue(file, entryOffset, tiffStart, dirStart, bigEnd) {
        var type = file.getUint16(entryOffset + 2, !bigEnd),
            numValues = file.getUint32(entryOffset + 4, !bigEnd),
            valueOffset = file.getUint32(entryOffset + 8, !bigEnd) + tiffStart,
            offset,
            vals, val, n,
            numerator, denominator;

        switch (type) {
            case 1: // byte, 8-bit unsigned int
            case 7: // undefined, 8-bit byte, value depending on field
                if (numValues == 1) {
                    return file.getUint8(entryOffset + 8, !bigEnd);
                } else {
                    offset = numValues > 4 ? valueOffset : (entryOffset + 8);
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        vals[n] = file.getUint8(offset + n);
                    }
                    return vals;
                }

            case 2: // ascii, 8-bit byte
                offset = numValues > 4 ? valueOffset : (entryOffset + 8);
                return getStringFromDB(file, offset, numValues - 1);

            case 3: // short, 16 bit int
                if (numValues == 1) {
                    return file.getUint16(entryOffset + 8, !bigEnd);
                } else {
                    offset = numValues > 2 ? valueOffset : (entryOffset + 8);
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        vals[n] = file.getUint16(offset + 2 * n, !bigEnd);
                    }
                    return vals;
                }

            case 4: // long, 32 bit int
                if (numValues == 1) {
                    return file.getUint32(entryOffset + 8, !bigEnd);
                } else {
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        vals[n] = file.getUint32(valueOffset + 4 * n, !bigEnd);
                    }
                    return vals;
                }

            case 5:    // rational = two long values, first is numerator, second is denominator
                if (numValues == 1) {
                    numerator = file.getUint32(valueOffset, !bigEnd);
                    denominator = file.getUint32(valueOffset + 4, !bigEnd);
                    val = new Number(numerator / denominator);
                    val.numerator = numerator;
                    val.denominator = denominator;
                    return val;
                } else {
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        numerator = file.getUint32(valueOffset + 8 * n, !bigEnd);
                        denominator = file.getUint32(valueOffset + 4 + 8 * n, !bigEnd);
                        vals[n] = new Number(numerator / denominator);
                        vals[n].numerator = numerator;
                        vals[n].denominator = denominator;
                    }
                    return vals;
                }

            case 9: // slong, 32 bit signed int
                if (numValues == 1) {
                    return file.getInt32(entryOffset + 8, !bigEnd);
                } else {
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        vals[n] = file.getInt32(valueOffset + 4 * n, !bigEnd);
                    }
                    return vals;
                }

            case 10: // signed rational, two slongs, first is numerator, second is denominator
                if (numValues == 1) {
                    return file.getInt32(valueOffset, !bigEnd) / file.getInt32(valueOffset + 4, !bigEnd);
                } else {
                    vals = [];
                    for (n = 0; n < numValues; n++) {
                        vals[n] = file.getInt32(valueOffset + 8 * n, !bigEnd) / file.getInt32(valueOffset + 4 + 8 * n, !bigEnd);
                    }
                    return vals;
                }
        }
    }

    function getStringFromDB(buffer, start, length) {
        var outstr = "";
        for (n = start; n < start + length; n++) {
            outstr += String.fromCharCode(buffer.getUint8(n));
        }
        return outstr;
    }

    function readEXIFData(file, start) {
        if (getStringFromDB(file, start, 4) != "Exif") {
            if (debug) console.log("Not valid EXIF data! " + getStringFromDB(file, start, 4));
            return false;
        }

        var bigEnd,
            tags, tag,
            exifData, gpsData,
            tiffOffset = start + 6;

        // test for TIFF validity and endianness
        if (file.getUint16(tiffOffset) == 0x4949) {
            bigEnd = false;
        } else if (file.getUint16(tiffOffset) == 0x4D4D) {
            bigEnd = true;
        } else {
            if (debug) console.log("Not valid TIFF data! (no 0x4949 or 0x4D4D)");
            return false;
        }

        if (file.getUint16(tiffOffset + 2, !bigEnd) != 0x002A) {
            if (debug) console.log("Not valid TIFF data! (no 0x002A)");
            return false;
        }

        var firstIFDOffset = file.getUint32(tiffOffset + 4, !bigEnd);

        if (firstIFDOffset < 0x00000008) {
            if (debug) console.log("Not valid TIFF data! (First offset less than 8)", file.getUint32(tiffOffset + 4, !bigEnd));
            return false;
        }

        tags = readTags(file, tiffOffset, tiffOffset + firstIFDOffset, TiffTags, bigEnd);

        if (tags.ExifIFDPointer) {
            exifData = readTags(file, tiffOffset, tiffOffset + tags.ExifIFDPointer, ExifTags, bigEnd);
            for (tag in exifData) {
                switch (tag) {
                    case "LightSource":
                    case "Flash":
                    case "MeteringMode":
                    case "ExposureProgram":
                    case "SensingMethod":
                    case "SceneCaptureType":
                    case "SceneType":
                    case "CustomRendered":
                    case "WhiteBalance":
                    case "GainControl":
                    case "Contrast":
                    case "Saturation":
                    case "Sharpness":
                    case "SubjectDistanceRange":
                    case "FileSource":
                        exifData[tag] = StringValues[tag][exifData[tag]];
                        break;

                    case "ExifVersion":
                    case "FlashpixVersion":
                        exifData[tag] = String.fromCharCode(exifData[tag][0], exifData[tag][1], exifData[tag][2], exifData[tag][3]);
                        break;

                    case "ComponentsConfiguration":
                        exifData[tag] =
                            StringValues.Components[exifData[tag][0]] +
                            StringValues.Components[exifData[tag][1]] +
                            StringValues.Components[exifData[tag][2]] +
                            StringValues.Components[exifData[tag][3]];
                        break;
                }
                tags[tag] = exifData[tag];
            }
        }

        if (tags.GPSInfoIFDPointer) {
            gpsData = readTags(file, tiffOffset, tiffOffset + tags.GPSInfoIFDPointer, GPSTags, bigEnd);
            for (tag in gpsData) {
                switch (tag) {
                    case "GPSVersionID":
                        gpsData[tag] = gpsData[tag][0] +
                            "." + gpsData[tag][1] +
                            "." + gpsData[tag][2] +
                            "." + gpsData[tag][3];
                        break;
                }
                tags[tag] = gpsData[tag];
            }
        }

        return tags;
    }

    EXIF.getData = function (img, callback) {
        if ((img instanceof Image || img instanceof HTMLImageElement) && !img.complete) return false;

        if (!imageHasData(img)) {
            getImageData(img, callback);
        } else {
            if (callback) {
                callback.call(img);
            }
        }
        return true;
    }

    EXIF.getTag = function (img, tag) {
        if (!imageHasData(img)) return;
        return img.exifdata[tag];
    }

    EXIF.getAllTags = function (img) {
        if (!imageHasData(img)) return {};
        var a,
            data = img.exifdata,
            tags = {};
        for (a in data) {
            if (data.hasOwnProperty(a)) {
                tags[a] = data[a];
            }
        }
        return tags;
    }

    EXIF.pretty = function (img) {
        if (!imageHasData(img)) return "";
        var a,
            data = img.exifdata,
            strPretty = "";
        for (a in data) {
            if (data.hasOwnProperty(a)) {
                if (typeof data[a] == "object") {
                    if (data[a] instanceof Number) {
                        strPretty += a + " : " + data[a] + " [" + data[a].numerator + "/" + data[a].denominator + "]\r\n";
                    } else {
                        strPretty += a + " : [" + data[a].length + " values]\r\n";
                    }
                } else {
                    strPretty += a + " : " + data[a] + "\r\n";
                }
            }
        }
        return strPretty;
    }

    EXIF.readFromBinaryFile = function (file) {
        return findEXIFinJPEG(file);
    }

    if (typeof define === 'function' && define.amd) {
        define('exif-js', [], function () {
            return EXIF;
        });
    }
}.call(this));

//------lrz.js
/**
 * lrz3
 * https://github.com/think2011/localResizeIMG3
 * @author think2011
 */
;
(function () {
    window.URL = window.URL || window.webkitURL;
    var userAgent = navigator.userAgent;

    /**
     * 客户端压缩图片
     * @param file
     * @param [options]
     * @param callback
     * @constructor
     */
    function Lrz(file, options, callback) {
        this.file = file;
        this.callback = callback;
        this.defaults = { quality: 7 };

        // 适应传入的参数
        if (callback) {
            for (var p in options) {
                this.defaults[p] = options[p];
            }
            if (this.defaults.quality > 10) this.defaults.quality = 10;
        } else {
            this.callback = options;
        }

        this.results = {
            blob: null,
            origin: null,
            base64: null
        };

        this.init();
    }

    Lrz.prototype = {
        constructor: Lrz,

        /**
         * 初始化
         */
        init: function () {
            var that = this;

            that.create(that.file, that.callback);
        },

        /**
         * 生成base64
         * @param file
         * @param callback
         */
        create: function (file, callback) {
            var that = this,
                img = new Image(),
                results = that.results,
                blob = file == "[object File]" ? URL.createObjectURL(file) : file;
            img.onload = function () {
                // 获得图片缩放尺寸
                var resize = that.resize(this);

                // 创建canvas
                var canvas = document.createElement('canvas'), ctx;
                canvas.width = resize.w;
                canvas.height = resize.h;
                ctx = canvas.getContext('2d');

                // 兼容 IOS
                if (/iphone/i.test(userAgent)) {
                    try {
                        var mpImg = new MegaPixImage(img);
                        mpImg.render(canvas, {
                            maxWidth: canvas.width,
                            maxHeight: canvas.height
                        });
                    } catch (_error) {
                        alert('未引用mobile补丁，无法生成图片。');
                    }
                }

                // 调整正确的拍摄方向
                EXIF.getData(img, function () {
                    var orientationEXIF = (EXIF.pretty(this)).match(/Orientation : (\d)/),
                        orientation = orientationEXIF ? +orientationEXIF[1] : 1;

                    switch (orientation) {
                        case 3:
                            ctx.rotate(180 * Math.PI / 180);
                            ctx.drawImage(img, -resize.w, -resize.h, resize.w, resize.h);
                            break;

                        case 6:
                            canvas.width = resize.h;
                            canvas.height = resize.w;
                            ctx.rotate(90 * Math.PI / 180);
                            ctx.drawImage(img, 0, -resize.h, resize.w, resize.h);
                            break;

                        case 8:
                            canvas.width = resize.h;
                            canvas.height = resize.w;
                            ctx.rotate(270 * Math.PI / 180);
                            ctx.drawImage(img, -resize.w, 0, resize.w, resize.h);
                            break;

                        default:
                            ctx.drawImage(img, 0, 0, resize.w, resize.h);

                    }

                    // 生成结果
                    results.blob = blob;
                    results.origin = file;

                    // 兼容 Android
                    if (/Android/i.test(userAgent)) {
                        try {
                            var encoder = new JPEGEncoder();

                            results.base64 = encoder.encode(ctx.getImageData(0, 0, canvas.width, canvas.height), that.defaults.quality * 100);
                        } catch (_error) {
                            alert('未引用mobile补丁，无法生成图片。');
                        }
                    }

                        // 其他情况&IOS
                    else {
                        results.base64 = canvas.toDataURL('image/jpeg', that.defaults.quality);
                    }

                    // 执行回调
                    callback(results);
                });
            };

            img.src = blob;
        },

        /**
         * 获得图片的缩放尺寸
         * @param img
         * @returns {{w: (Number), h: (Number)}}
         */
        resize: function (img) {
            var w = this.defaults.width,
                h = this.defaults.height,
                scale = img.width / img.height,
                ret = { w: img.width, h: img.height };

            if (w & h) {
                ret.w = w;
                ret.h = h;
            }
            else if (w) {
                ret.w = w;
                ret.h = Math.ceil(w / scale);
            }

            else if (h) {
                ret.w = Math.ceil(h * scale);
                ret.h = h;
            }

            return ret;
        }
    };

    // 暴露接口
    window.lrz = function (file, options, callback) {
        return new Lrz(file, options, callback);
    };
})()


//lrz(obj.files[0], { width: 400, quality: 0.7 }, function (results) {
//    SFileUP.AjaxUpBase64(results.base64, function (data) {

//    });
//});