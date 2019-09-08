#region LICENSE
/*
 * Copyright (C) 2004 - 2007 David Hudson (jendave@yahoo.com)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
#endregion LICENSE

using System;
using System.Diagnostics.CodeAnalysis;

//using Tao.Sdl;
using SdlDotNet.Core;
using SdlDotNet.Graphics;

namespace SdlDotNet.Input
{
    #region Key

    /// <summary>
    /// What we really want is a mapping of every raw key on the keyboard.
    /// To support international keyboards, we use the range 0xA1 - 0xFF
    /// as international virtual keycodes.  
    /// We'll follow in the footsteps of X11...
    /// The keyboard syms have been cleverly chosen to map to ASCII
    /// </summary>
    /// <remarks></remarks>
    public enum Key
    {
        /// <summary>
        ///Unknown
        /// </summary>
        Unknown = Sdl.SDLK_UNKNOWN,
        /// <summary>
        /// first
        /// </summary>
        First = Sdl.SDLK_FIRST,
        /// <summary>
        /// backspace. '\b'
        /// </summary>
        Backspace = Sdl.SDLK_BACKSPACE,
        /// <summary>
        /// tab. '\t'
        /// </summary>
        Tab = Sdl.SDLK_TAB,
        /// <summary>
        /// clear
        /// </summary>
        Clear = Sdl.SDLK_CLEAR,
        /// <summary>
        /// return. '\r'
        /// </summary>
        Return = Sdl.SDLK_RETURN,
        /// <summary>
        /// pause
        /// </summary>
        Pause = Sdl.SDLK_PAUSE,
        /// <summary>
        /// escape. '^['
        /// </summary>
        Escape = Sdl.SDLK_ESCAPE,
        /// <summary>
        /// space. ' '
        /// </summary>
        Space = Sdl.SDLK_SPACE,
        /// <summary>
        /// exclaim. '!'
        /// </summary>
        ExclamationPoint = Sdl.SDLK_EXCLAIM,
        /// <summary>
        /// quotedbl. '"'
        /// </summary>
        DoubleQuote = Sdl.SDLK_QUOTEDBL,
        /// <summary>
        /// hash. '#'
        /// </summary>
        Hash = Sdl.SDLK_HASH,
        /// <summary>
        /// dollar. '$'
        /// </summary>
        DollarSign = Sdl.SDLK_DOLLAR,
        /// <summary>
        /// ampersand. '&amp;'
        /// </summary>
        Ampersand = Sdl.SDLK_AMPERSAND,
        /// <summary>
        /// quote. '''
        /// </summary>
        Quote = Sdl.SDLK_QUOTE,
        /// <summary>
        /// left parenthesis. '('
        /// </summary>
        LeftParenthesis = Sdl.SDLK_LEFTPAREN,
        /// <summary>
        /// right parenthesis. ')'
        /// </summary>
        RightParenthesis = Sdl.SDLK_RIGHTPAREN,
        /// <summary>
        /// asterisk. '*'
        /// </summary>
        Asterisk = Sdl.SDLK_ASTERISK,
        /// <summary>
        /// plus sign. '+'
        /// </summary>
        Plus = Sdl.SDLK_PLUS,
        /// <summary>
        /// comma. ','
        /// </summary>
        Comma = Sdl.SDLK_COMMA,
        /// <summary>
        /// minus sign. '-'
        /// </summary>
        Minus = Sdl.SDLK_MINUS,
        /// <summary>
        /// period. '.'
        /// </summary>
        Period = Sdl.SDLK_PERIOD,
        /// <summary>
        /// forward slash. '/'
        /// </summary>
        Slash = Sdl.SDLK_SLASH,
        /// <summary>
        /// 0
        /// </summary>
        Zero = Sdl.SDLK_0,
        /// <summary>
        /// 1
        /// </summary>
        One = Sdl.SDLK_1,
        /// <summary>
        /// 2
        /// </summary>
        Two = Sdl.SDLK_2,
        /// <summary>
        /// 3
        /// </summary>
        Three = Sdl.SDLK_3,
        /// <summary>
        /// 4
        /// </summary>
        Four = Sdl.SDLK_4,
        /// <summary>
        /// 5
        /// </summary>
        Five = Sdl.SDLK_5,
        /// <summary>
        /// 6
        /// </summary>
        Six = Sdl.SDLK_6,
        /// <summary>
        /// 7
        /// </summary>
        Seven = Sdl.SDLK_7,
        /// <summary>
        /// 8
        /// </summary>
        Eight = Sdl.SDLK_8,
        /// <summary>
        /// 9
        /// </summary>
        Nine = Sdl.SDLK_9,
        /// <summary>
        /// colon. ':'
        /// </summary>
        Colon = Sdl.SDLK_COLON,
        /// <summary>
        /// semicolon. ';'
        /// </summary>
        Semicolon = Sdl.SDLK_SEMICOLON,
        /// <summary>
        /// less-than sign. '&lt;'
        /// </summary>
        LessThan = Sdl.SDLK_LESS,
        /// <summary>
        /// equals sign. '='
        /// </summary>
        Equals = Sdl.SDLK_EQUALS,
        /// <summary>
        /// greater-than sign. '&gt;'
        /// </summary>
        GreaterThan = Sdl.SDLK_GREATER,
        /// <summary>
        /// question mark. '?'
        /// </summary>
        QuestionMark = Sdl.SDLK_QUESTION,
        /// <summary>
        /// at. '@'
        /// </summary>
        At = Sdl.SDLK_AT,
        /*
        Skip uppercase letters
        */
        /// <summary>
        /// left bracket. '['
        /// </summary>
        LeftBracket = Sdl.SDLK_LEFTBRACKET,
        /// <summary>
        /// backslash. '\'
        /// </summary>
        Backslash = Sdl.SDLK_BACKSLASH,
        /// <summary>
        /// right bracket. ']'
        /// </summary>
        RightBracket = Sdl.SDLK_RIGHTBRACKET,
        /// <summary>
        /// caret. '^'
        /// </summary>
        Caret = Sdl.SDLK_CARET,
        /// <summary>
        /// underscore.'_'
        /// </summary>
        Underscore = Sdl.SDLK_UNDERSCORE,
        /// <summary>
        /// grave. '`'
        /// </summary>
        BackQuote = Sdl.SDLK_BACKQUOTE,
        /// <summary>
        /// a
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        A = Sdl.SDLK_a,
        /// <summary>
        /// b
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        B = Sdl.SDLK_b,
        /// <summary>
        /// c
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        C = Sdl.SDLK_c,
        /// <summary>
        /// d
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        D = Sdl.SDLK_d,
        /// <summary>
        /// e
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        E = Sdl.SDLK_e,
        /// <summary>
        /// f
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        F = Sdl.SDLK_f,
        /// <summary>
        /// g
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        G = Sdl.SDLK_g,
        /// <summary>
        /// h
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        H = Sdl.SDLK_h,
        /// <summary>
        /// i
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        I = Sdl.SDLK_i,
        /// <summary>
        /// j
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        J = Sdl.SDLK_j,
        /// <summary>
        /// k
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        K = Sdl.SDLK_k,
        /// <summary>
        /// l
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        L = Sdl.SDLK_l,
        /// <summary>
        /// m
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        M = Sdl.SDLK_m,
        /// <summary>
        /// n
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        N = Sdl.SDLK_n,
        /// <summary>
        /// o
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        O = Sdl.SDLK_o,
        /// <summary>
        /// p
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        P = Sdl.SDLK_p,
        /// <summary>
        /// q
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        Q = Sdl.SDLK_q,
        /// <summary>
        /// r
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        R = Sdl.SDLK_r,
        /// <summary>
        /// s
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        S = Sdl.SDLK_s,
        /// <summary>
        /// t
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        T = Sdl.SDLK_t,
        /// <summary>
        /// u
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        U = Sdl.SDLK_u,
        /// <summary>
        /// v
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        V = Sdl.SDLK_v,
        /// <summary>
        /// w
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        W = Sdl.SDLK_w,
        /// <summary>
        /// x
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        X = Sdl.SDLK_x,
        /// <summary>
        /// y
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        Y = Sdl.SDLK_y,
        /// <summary>
        /// z
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        Z = Sdl.SDLK_z,
        /// <summary>
        /// delete. '^?'
        /// </summary>
        Delete = Sdl.SDLK_DELETE,
        /* End of ASCII mapped keysyms */

        /* International keyboard syms */
        /// <summary>
        /// 0xA0
        /// </summary>
        World0 = Sdl.SDLK_WORLD_0,
        /// <summary>
        ///
        /// </summary>
        World1 = Sdl.SDLK_WORLD_1,
        /// <summary>
        ///
        /// </summary>
        World2 = Sdl.SDLK_WORLD_2,
        /// <summary>
        ///
        /// </summary>
        World3 = Sdl.SDLK_WORLD_3,
        /// <summary>
        ///
        /// </summary>
        World4 = Sdl.SDLK_WORLD_4,
        /// <summary>
        ///
        /// </summary>
        World5 = Sdl.SDLK_WORLD_5,
        /// <summary>
        ///
        /// </summary>
        World6 = Sdl.SDLK_WORLD_6,
        /// <summary>
        ///
        /// </summary>
        World7 = Sdl.SDLK_WORLD_7,
        /// <summary>
        ///
        /// </summary>
        World8 = Sdl.SDLK_WORLD_8,
        /// <summary>
        ///
        /// </summary>
        World9 = Sdl.SDLK_WORLD_9,
        /// <summary>
        ///
        /// </summary>
        World10 = Sdl.SDLK_WORLD_10,
        /// <summary>
        ///
        /// </summary>
        World11 = Sdl.SDLK_WORLD_11,
        /// <summary>
        ///
        /// </summary>
        World12 = Sdl.SDLK_WORLD_12,
        /// <summary>
        ///
        /// </summary>
        World13 = Sdl.SDLK_WORLD_13,
        /// <summary>
        ///
        /// </summary>
        World14 = Sdl.SDLK_WORLD_14,
        /// <summary>
        ///
        /// </summary>
        World15 = Sdl.SDLK_WORLD_15,
        /// <summary>
        ///
        /// </summary>
        World16 = Sdl.SDLK_WORLD_16,
        /// <summary>
        ///
        /// </summary>
        World17 = Sdl.SDLK_WORLD_17,
        /// <summary>
        ///
        /// </summary>
        World18 = Sdl.SDLK_WORLD_18,
        /// <summary>
        ///
        /// </summary>
        World19 = Sdl.SDLK_WORLD_19,
        /// <summary>
        ///
        /// </summary>
        World20 = Sdl.SDLK_WORLD_20,
        /// <summary>
        ///
        /// </summary>
        World21 = Sdl.SDLK_WORLD_21,
        /// <summary>
        ///
        /// </summary>
        World22 = Sdl.SDLK_WORLD_22,
        /// <summary>
        ///
        /// </summary>
        World23 = Sdl.SDLK_WORLD_23,
        /// <summary>
        ///
        /// </summary>
        World24 = Sdl.SDLK_WORLD_24,
        /// <summary>
        ///
        /// </summary>
        World25 = Sdl.SDLK_WORLD_25,
        /// <summary>
        ///
        /// </summary>
        World26 = Sdl.SDLK_WORLD_26,
        /// <summary>
        ///
        /// </summary>
        World27 = Sdl.SDLK_WORLD_27,
        /// <summary>
        ///
        /// </summary>
        World28 = Sdl.SDLK_WORLD_28,
        /// <summary>
        ///
        /// </summary>
        World29 = Sdl.SDLK_WORLD_29,
        /// <summary>
        ///
        /// </summary>
        World30 = Sdl.SDLK_WORLD_30,
        /// <summary>
        ///
        /// </summary>
        World31 = Sdl.SDLK_WORLD_31,
        /// <summary>
        ///
        /// </summary>
        World32 = Sdl.SDLK_WORLD_32,
        /// <summary>
        ///
        /// </summary>
        World33 = Sdl.SDLK_WORLD_33,
        /// <summary>
        ///
        /// </summary>
        World34 = Sdl.SDLK_WORLD_34,
        /// <summary>
        ///
        /// </summary>
        World35 = Sdl.SDLK_WORLD_35,
        /// <summary>
        ///
        /// </summary>
        World36 = Sdl.SDLK_WORLD_36,
        /// <summary>
        ///
        /// </summary>
        World37 = Sdl.SDLK_WORLD_37,
        /// <summary>
        ///
        /// </summary>
        World38 = Sdl.SDLK_WORLD_38,
        /// <summary>
        ///
        /// </summary>
        World39 = Sdl.SDLK_WORLD_39,
        /// <summary>
        ///
        /// </summary>
        World40 = Sdl.SDLK_WORLD_40,
        /// <summary>
        ///
        /// </summary>
        World41 = Sdl.SDLK_WORLD_41,
        /// <summary>
        ///
        /// </summary>
        World42 = Sdl.SDLK_WORLD_42,
        /// <summary>
        ///
        /// </summary>
        World43 = Sdl.SDLK_WORLD_43,
        /// <summary>
        ///
        /// </summary>
        World44 = Sdl.SDLK_WORLD_44,
        /// <summary>
        ///
        /// </summary>
        World45 = Sdl.SDLK_WORLD_45,
        /// <summary>
        ///
        /// </summary>
        World46 = Sdl.SDLK_WORLD_46,
        /// <summary>
        ///
        /// </summary>
        World47 = Sdl.SDLK_WORLD_47,
        /// <summary>
        ///
        /// </summary>
        World48 = Sdl.SDLK_WORLD_48,
        /// <summary>
        ///
        /// </summary>
        World49 = Sdl.SDLK_WORLD_49,
        /// <summary>
        ///
        /// </summary>
        World50 = Sdl.SDLK_WORLD_50,
        /// <summary>
        ///
        /// </summary>
        World51 = Sdl.SDLK_WORLD_51,
        /// <summary>
        ///
        /// </summary>
        World52 = Sdl.SDLK_WORLD_52,
        /// <summary>
        ///
        /// </summary>
        World53 = Sdl.SDLK_WORLD_53,
        /// <summary>
        ///
        /// </summary>
        World54 = Sdl.SDLK_WORLD_54,
        /// <summary>
        ///
        /// </summary>
        World55 = Sdl.SDLK_WORLD_55,
        /// <summary>
        ///
        /// </summary>
        World56 = Sdl.SDLK_WORLD_56,
        /// <summary>
        ///
        /// </summary>
        World57 = Sdl.SDLK_WORLD_57,
        /// <summary>
        ///
        /// </summary>
        World58 = Sdl.SDLK_WORLD_58,
        /// <summary>
        ///
        /// </summary>
        World59 = Sdl.SDLK_WORLD_59,
        /// <summary>
        ///
        /// </summary>
        World60 = Sdl.SDLK_WORLD_60,
        /// <summary>
        ///
        /// </summary>
        World61 = Sdl.SDLK_WORLD_61,
        /// <summary>
        ///
        /// </summary>
        World62 = Sdl.SDLK_WORLD_62,
        /// <summary>
        ///
        /// </summary>
        World63 = Sdl.SDLK_WORLD_63,
        /// <summary>
        ///
        /// </summary>
        World64 = Sdl.SDLK_WORLD_64,
        /// <summary>
        ///
        /// </summary>
        World65 = Sdl.SDLK_WORLD_65,
        /// <summary>
        ///
        /// </summary>
        World66 = Sdl.SDLK_WORLD_66,
        /// <summary>
        ///
        /// </summary>
        World67 = Sdl.SDLK_WORLD_67,
        /// <summary>
        ///
        /// </summary>
        World68 = Sdl.SDLK_WORLD_68,
        /// <summary>
        ///
        /// </summary>
        World69 = Sdl.SDLK_WORLD_69,
        /// <summary>
        ///
        /// </summary>
        World70 = Sdl.SDLK_WORLD_70,
        /// <summary>
        ///
        /// </summary>
        World71 = Sdl.SDLK_WORLD_71,
        /// <summary>
        ///
        /// </summary>
        World72 = Sdl.SDLK_WORLD_72,
        /// <summary>
        ///
        /// </summary>
        World73 = Sdl.SDLK_WORLD_73,
        /// <summary>
        ///
        /// </summary>
        World74 = Sdl.SDLK_WORLD_74,
        /// <summary>
        ///
        /// </summary>
        World75 = Sdl.SDLK_WORLD_75,
        /// <summary>
        ///
        /// </summary>
        World76 = Sdl.SDLK_WORLD_76,
        /// <summary>
        ///
        /// </summary>
        World77 = Sdl.SDLK_WORLD_77,
        /// <summary>
        ///
        /// </summary>
        World78 = Sdl.SDLK_WORLD_78,
        /// <summary>
        ///
        /// </summary>
        World79 = Sdl.SDLK_WORLD_79,
        /// <summary>
        ///
        /// </summary>
        World80 = Sdl.SDLK_WORLD_80,
        /// <summary>
        ///
        /// </summary>
        World81 = Sdl.SDLK_WORLD_81,
        /// <summary>
        ///
        /// </summary>
        World82 = Sdl.SDLK_WORLD_82,
        /// <summary>
        ///
        /// </summary>
        World83 = Sdl.SDLK_WORLD_83,
        /// <summary>
        ///
        /// </summary>
        World84 = Sdl.SDLK_WORLD_84,
        /// <summary>
        ///
        /// </summary>
        World85 = Sdl.SDLK_WORLD_85,
        /// <summary>
        ///
        /// </summary>
        World86 = Sdl.SDLK_WORLD_86,
        /// <summary>
        ///
        /// </summary>
        World87 = Sdl.SDLK_WORLD_87,
        /// <summary>
        ///
        /// </summary>
        World88 = Sdl.SDLK_WORLD_88,
        /// <summary>
        ///
        /// </summary>
        World89 = Sdl.SDLK_WORLD_89,
        /// <summary>
        ///
        /// </summary>
        World90 = Sdl.SDLK_WORLD_90,
        /// <summary>
        ///
        /// </summary>
        World91 = Sdl.SDLK_WORLD_91,
        /// <summary>
        ///
        /// </summary>
        World92 = Sdl.SDLK_WORLD_92,
        /// <summary>
        ///
        /// </summary>
        World93 = Sdl.SDLK_WORLD_93,
        /// <summary>
        ///
        /// </summary>
        World94 = Sdl.SDLK_WORLD_94,
        /// <summary>
        /// 0xFF
        /// </summary>
        World95 = Sdl.SDLK_WORLD_95,

        /* Numeric keypad */
        /// <summary>
        /// keypad 0
        /// </summary>
        Keypad0 = Sdl.SDLK_KP0,
        /// <summary>
        /// keypad 1
        /// </summary>
        Keypad1 = Sdl.SDLK_KP1,
        /// <summary>
        /// keypad 2
        /// </summary>
        Keypad2 = Sdl.SDLK_KP2,
        /// <summary>
        /// keypad 3
        /// </summary>
        Keypad3 = Sdl.SDLK_KP3,
        /// <summary>
        /// keypad 4
        /// </summary>
        Keypad4 = Sdl.SDLK_KP4,
        /// <summary>
        /// keypad 5
        /// </summary>
        Keypad5 = Sdl.SDLK_KP5,
        /// <summary>
        /// keypad 6
        /// </summary>
        Keypad6 = Sdl.SDLK_KP6,
        /// <summary>
        /// keypad 7
        /// </summary>
        Keypad7 = Sdl.SDLK_KP7,
        /// <summary>
        /// keypad 8
        /// </summary>
        Keypad8 = Sdl.SDLK_KP8,
        /// <summary>
        /// keypad 9
        /// </summary>
        Keypad9 = Sdl.SDLK_KP9,
        /// <summary>
        /// keypad period. '.'
        /// </summary>
        KeypadPeriod = Sdl.SDLK_KP_PERIOD,
        /// <summary>
        /// keypad divide. '/'
        /// </summary>
        KeypadDivide = Sdl.SDLK_KP_DIVIDE,
        /// <summary>
        /// keypad multiply. '*'
        /// </summary>
        KeypadMultiply = Sdl.SDLK_KP_MULTIPLY,
        /// <summary>
        /// keypad minus. '-'
        /// </summary>
        KeypadMinus = Sdl.SDLK_KP_MINUS,
        /// <summary>
        /// keypad plus. '+'
        /// </summary>
        KeypadPlus = Sdl.SDLK_KP_PLUS,
        /// <summary>
        /// keypad enter. '\r'
        /// </summary>
        KeypadEnter = Sdl.SDLK_KP_ENTER,
        /// <summary>
        /// keypad equals. '='
        /// </summary>
        KeypadEquals = Sdl.SDLK_KP_EQUALS,

        /* Arrows + Home/End pad */
        /// <summary>
        /// up arrow
        /// </summary>
        UpArrow = Sdl.SDLK_UP,
        /// <summary>
        /// down arrow
        /// </summary>
        DownArrow = Sdl.SDLK_DOWN,
        /// <summary>
        /// right arrow
        /// </summary>
        RightArrow = Sdl.SDLK_RIGHT,
        /// <summary>
        /// left arrow
        /// </summary>
        LeftArrow = Sdl.SDLK_LEFT,
        /// <summary>
        /// insert
        /// </summary>
        Insert = Sdl.SDLK_INSERT,
        /// <summary>
        /// home
        /// </summary>
        Home = Sdl.SDLK_HOME,
        /// <summary>
        /// end
        /// </summary>
        End = Sdl.SDLK_END,
        /// <summary>
        /// page up
        /// </summary>
        PageUp = Sdl.SDLK_PAGEUP,
        /// <summary>
        /// page down
        /// </summary>
        PageDown = Sdl.SDLK_PAGEDOWN,

        /* Function keys */
        /// <summary>
        /// F1
        /// </summary>
        F1 = Sdl.SDLK_F1,
        /// <summary>
        /// F2
        /// </summary>
        F2 = Sdl.SDLK_F2,
        /// <summary>
        /// F3
        /// </summary>
        F3 = Sdl.SDLK_F3,
        /// <summary>
        /// F4
        /// </summary>
        F4 = Sdl.SDLK_F4,
        /// <summary>
        /// F5
        /// </summary>
        F5 = Sdl.SDLK_F5,
        /// <summary>
        /// F6
        /// </summary>
        F6 = Sdl.SDLK_F6,
        /// <summary>
        /// F7
        /// </summary>
        F7 = Sdl.SDLK_F7,
        /// <summary>
        /// F8
        /// </summary>
        F8 = Sdl.SDLK_F8,
        /// <summary>
        /// F9
        /// </summary>
        F9 = Sdl.SDLK_F9,
        /// <summary>
        /// F10
        /// </summary>
        F10 = Sdl.SDLK_F10,
        /// <summary>
        /// F11
        /// </summary>
        F11 = Sdl.SDLK_F11,
        /// <summary>
        /// F12
        /// </summary>
        F12 = Sdl.SDLK_F12,
        /// <summary>
        /// F13
        /// </summary>
        F13 = Sdl.SDLK_F13,
        /// <summary>
        /// F14
        /// </summary>
        F14 = Sdl.SDLK_F14,
        /// <summary>
        /// F15
        /// </summary>
        F15 = Sdl.SDLK_F15,

        /* Key state modifier keys */
        /// <summary>
        /// numlock
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        NumLock = Sdl.SDLK_NUMLOCK,
        /// <summary>
        /// capslock
        /// </summary>
        CapsLock = Sdl.SDLK_CAPSLOCK,
        /// <summary>
        /// scrollock
        /// </summary>
        ScrollLock = Sdl.SDLK_SCROLLOCK,
        /// <summary>
        /// right shift
        /// </summary>
        RightShift = Sdl.SDLK_RSHIFT,
        /// <summary>
        /// left shift
        /// </summary>
        LeftShift = Sdl.SDLK_LSHIFT,
        /// <summary>
        /// right ctrl
        /// </summary>
        RightControl = Sdl.SDLK_RCTRL,
        /// <summary>
        /// left ctrl
        /// </summary>
        LeftControl = Sdl.SDLK_LCTRL,
        /// <summary>
        /// right alt
        /// </summary>
        RightAlt = Sdl.SDLK_RALT,
        /// <summary>
        /// left alt
        /// </summary>
        LeftAlt = Sdl.SDLK_LALT,
        /// <summary>
        /// right meta
        /// </summary>
        RightMeta = Sdl.SDLK_RMETA,
        /// <summary>
        /// left meta
        /// </summary>
        LeftMeta = Sdl.SDLK_LMETA,
        /// <summary>
        /// Left "Windows" key
        /// </summary>
        LeftWindows = Sdl.SDLK_LSUPER,
        /// <summary>
        /// Right "Windows" key
        /// </summary>
        RightWindows = Sdl.SDLK_RSUPER,
        /// <summary>
        /// "Alt Gr" key. Mode key
        /// </summary>
        Mode = Sdl.SDLK_MODE,
        /// <summary>
        /// Multi-key compose key
        /// </summary>
        Compose = Sdl.SDLK_COMPOSE,

        // Miscellaneous function keys
        /// <summary>
        /// help
        /// </summary>
        Help = Sdl.SDLK_HELP,
        /// <summary>
        /// print-screen
        /// </summary>
        Print = Sdl.SDLK_PRINT,
        /// <summary>
        /// SysRq
        /// </summary>
        SysRQ = Sdl.SDLK_SYSREQ,
        /// <summary>
        /// break
        /// </summary>
        Break = Sdl.SDLK_BREAK,
        /// <summary>
        /// menu
        /// </summary>
        Menu = Sdl.SDLK_MENU,
        /// <summary>
        /// Power Macintosh power key
        /// </summary>
        Power = Sdl.SDLK_POWER,
        /// <summary>
        /// Some european keyboards
        /// </summary>
        Euro = Sdl.SDLK_EURO,
        /// <summary>
        /// Atari keyboard has Undo
        /// </summary>
        Undo = Sdl.SDLK_UNDO,

        // Add any other keys here
        /// <summary>
        ///
        /// </summary>
        Last = Sdl.SDLK_LAST
    }

    #endregion Key

    #region ModifierKeys

    /// <summary>
    /// Enumeration of valid key mods (possibly OR'd together) 
    /// </summary>
    /// <remarks></remarks>
    [FlagsAttribute]
    public enum ModifierKeys
    {
        /// <summary>
        /// No modifiers applicable
        /// </summary>
        None = Sdl.KMOD_NONE,
        /// <summary>
        /// Left Shift is down
        /// </summary>
        LeftShift = Sdl.KMOD_LSHIFT,
        /// <summary>
        /// Right Shift is down
        /// </summary>
        RightShift = Sdl.KMOD_RSHIFT,
        /// <summary>
        /// Left Control is down
        /// </summary>
        LeftControl = Sdl.KMOD_LCTRL,
        /// <summary>
        /// Right Control is down
        /// </summary>
        RightControl = Sdl.KMOD_RCTRL,
        /// <summary>
        /// Left Alt is down
        /// </summary>
        LeftAlt = Sdl.KMOD_LALT,
        /// <summary>
        /// Right Alt is down
        /// </summary>
        RightAlt = Sdl.KMOD_RALT,
        /// <summary>
        /// Left Meta is down
        /// </summary>
        LeftMeta = Sdl.KMOD_LMETA,
        /// <summary>
        /// Right Meta is down
        /// </summary>
        RightMeta = Sdl.KMOD_RMETA,
        /// <summary>
        /// Numlock is down
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Correct Spelling")]
        NumLock = Sdl.KMOD_NUM,
        /// <summary>
        /// Capslock is down
        /// </summary>
        Caps = Sdl.KMOD_CAPS,
        /// <summary>
        /// 
        /// </summary>
        Mode = Sdl.KMOD_MODE,
        /// <summary>
        /// 
        /// </summary>
        ReserveKey = Sdl.KMOD_RESERVED,
        /// <summary>
        /// Both CTRL Keys
        /// </summary>
        ControlKeys = (Sdl.KMOD_LCTRL | Sdl.KMOD_RCTRL),
        /// <summary>
        /// Both SHIFT keys.
        /// </summary>
        ShiftKeys = (Sdl.KMOD_LSHIFT | Sdl.KMOD_RSHIFT),
        /// <summary>
        /// Both ALT keys.
        /// </summary>
        AltKeys = (Sdl.KMOD_LALT | Sdl.KMOD_RALT),
        /// <summary>
        /// Both META keys.
        /// </summary>
        MetaKeys = (Sdl.KMOD_LMETA | Sdl.KMOD_RMETA)
    }

    #endregion ModifierKeys

    #region Focus

    /// <summary>
    /// Type of Focus 
    /// </summary>
    /// <remarks>For the AppActive event.</remarks>
    [FlagsAttribute]
    public enum Focus
    {
        /// <summary>
        /// Mouse focus
        /// </summary>
        Mouse = Sdl.SDL_APPMOUSEFOCUS,
        /// <summary>
        /// Keeyboard focus
        /// </summary>
        Keyboard = Sdl.SDL_APPINPUTFOCUS,
        /// <summary>
        /// App becomes un-iconified.
        /// </summary>
        Application = Sdl.SDL_APPACTIVE
    }

    #endregion

    /// <summary>
    /// Keyboard class
    /// </summary>
    public static class Keyboard
    {
        #region Private fields

        static bool isInitialized = Video.Initialize();

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized
        {
            get { return Keyboard.isInitialized; }
        }

        /// <summary>
        /// Enable keyboard autorepeat
        /// </summary>
        /// <param name="delay">
        /// Delay in system ticks before repeat starts. 
        /// Set to 0 to disable key repeat.
        /// </param>
        /// <param name="rate">
        /// Rate in system ticks at which key repeats.
        /// </param>
        /// <remarks>This method will initialize the Video subsystem as well.</remarks>
        public static void EnableKeyRepeat(int delay, int rate)
        {
            if (Sdl.SDL_EnableKeyRepeat(delay, rate) == (int)SdlFlag.Error)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Determines if Key Repeating is turned on.
        /// </summary>
        public static bool KeyRepeat
        {
            set
            {
                if (value)
                {
                    EnableKeyRepeat(Sdl.SDL_DEFAULT_REPEAT_DELAY, Sdl.SDL_DEFAULT_REPEAT_INTERVAL);
                }
                else
                {
                    Sdl.SDL_EnableKeyRepeat(0, Sdl.SDL_DEFAULT_REPEAT_INTERVAL);
                }
            }
            get
            {
                int delay;
                int rate;
                Sdl.SDL_GetKeyRepeat(out rate, out delay);
                if (delay != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns true if the application has keyboard focus.
        /// </summary>
        public static bool HasFocus
        {
            get
            {
                return (Sdl.SDL_GetAppState() & Sdl.SDL_APPINPUTFOCUS) != 0;
            }
        }

        /// <summary>
        /// Returns the actual keyboard character that was pressed.
        /// </summary>
        /// <param name="key">Key to translate into the actual keyboard character.</param>
        /// <returns>Actual keyvboard character that was pressed.</returns>
        public static string KeyboardCharacter(Key key)
        {
            return Sdl.SDL_GetKeyName((int)key);
        }

        /// <summary>
        /// Returns which modifier keys are pressed
        /// </summary>
        public static ModifierKeys ModifierKeyState
        {
            get
            {
                return (ModifierKeys)Sdl.SDL_GetModState();
            }
            set
            {
                Sdl.SDL_SetModState((int)value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("Instead, consider just casting your System.Int16 to System.Char, or using System.Char.ConvertFromUtf32()", false)]
        public static string UnicodeCharacter(short input)
        {
            return new string((char)input, 1);
        } 


        /// <summary>
        /// Checks key state
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if key is pressed</returns>
        public static bool IsKeyPressed(Key key)
        {
            int numberOfKeys;
            byte[] keys;
            keys = Sdl.SDL_GetKeyState(out numberOfKeys);
            if (keys[(int)key] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if Unicode is enabled
        /// </summary>
        public static bool UnicodeEnabled
        {
            get
            {
                return (Sdl.SDL_EnableUNICODE(-1) == 1);
            }
            set
            {
                if (value == true)
                {
                    Sdl.SDL_EnableUNICODE(1);
                }
                else
                {
                    Sdl.SDL_EnableUNICODE(0);
                }
            }
        }

        #endregion
    }
}
